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

    protected async Task<TResponse> HandleRequest<TUnitOfWork, TResponse>(TUnitOfWork uow, object request, CancellationToken cancellationToken)
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
}

public class CQRSRequestHandler<TUnitOfWork> : CQRSRequestHandler, ICQRSRequestHandler<TUnitOfWork>
{
    public CQRSRequestHandler(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public Task HandleCommand(TUnitOfWork uow, ICommand command, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, EmptyResult>(uow, command, cancellationToken);
    }


    public Task<TResponse> HandleCommand<TResponse>(TUnitOfWork uow, ICommand<TResponse> command, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, TResponse>(uow, command, cancellationToken);
    }

    public Task<TResponse> HandleQuery<TResponse>(TUnitOfWork uow, IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        return HandleRequest<TUnitOfWork, TResponse>(uow, query, cancellationToken);
    }
}
