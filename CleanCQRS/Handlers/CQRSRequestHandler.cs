namespace CleanCQRS.Handlers;

public class CQRSRequestHandler : ICQRSRequestHandler
{
    private readonly IServiceProvider _serviceProvider;

    public CQRSRequestHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> HandleQuery<TUnitOfWork, TResponse>(TUnitOfWork uow, IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, TResponse>(uow, query, cancellationToken);
    }

    public Task HandleCommand<TUnitOfWork>(TUnitOfWork uow, ICommand command, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, EmptyResult>(uow, command, cancellationToken);
    }

    public Task<TResponse> HandleCommand<TUnitOfWork, TResponse>(TUnitOfWork uow, ICommand<TResponse> command, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, TResponse>(uow, command, cancellationToken);
    }

    private async Task<TResponse> HandleRequest<TUnitOfWork, TResponse>(TUnitOfWork uow, object request, CancellationToken cancellationToken)
    {
        var requestType = request.GetType();
        var requestName = requestType.FullName;

        var uowType = typeof(TUnitOfWork);
        var resultType = typeof(TResponse);

        var handlerType = typeof(IRequestHandler<,,>).MakeGenericType(uowType, requestType, resultType);
        var pipelineType = typeof(IPipeline<,,>).MakeGenericType(uowType, requestType, resultType);

        var handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Handler for type {requestName} returning {resultType.FullName} not registered.");

        var pipeline = _serviceProvider.GetService(pipelineType);

        var wrappedHandlerType = typeof(WrapperRequestHandler<,,>).MakeGenericType(typeof(TUnitOfWork), requestType, resultType);
        var wrappedHandler = (IRunnable<TUnitOfWork, TResponse>)Activator.CreateInstance(wrappedHandlerType, handler, pipeline)!;

        return await wrappedHandler.Run(uow, request, cancellationToken);
    }

    /// <summary>
    /// This class in needed to cast the IRequest<TResponse> request which is known at build time to the TRequest which is only known at run time
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    private class WrapperRequestHandler<TUnitOfWork, TRequest, TResponse> : IRunnable<TUnitOfWork, TResponse>
    {
        private readonly IRequestHandler<TUnitOfWork, TRequest, TResponse> _handler;
        private readonly IPipeline<TUnitOfWork, TRequest, TResponse>? _pipeline;

        public WrapperRequestHandler(IRequestHandler<TUnitOfWork, TRequest, TResponse> handler, IPipeline<TUnitOfWork, TRequest, TResponse>? pipeline)
        {
            _handler = handler;
            _pipeline = pipeline;
        }

        public Task<TResponse> Run(TUnitOfWork uow, object request, CancellationToken cancellationToken)
        {
            if (_pipeline == null)
            {
                return _handler.Run(uow, (TRequest)request, cancellationToken);
            }
            return _pipeline.Run(uow, (TRequest)request, _handler, cancellationToken);
        }
    }

    private interface IRunnable<TUnitOfWork, TResponse>
    {
        Task<TResponse> Run(TUnitOfWork uow, object request, CancellationToken cancellationToken);
    }
}
