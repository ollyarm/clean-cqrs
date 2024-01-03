namespace CleanCQRS.Handlers;

/// <summary>
/// This class in needed to cast the IRequest<TResponse> request which is known at build time to the TRequest which is only known at run time
/// </summary>
/// <typeparam name="TRequest"></typeparam>
internal class WrapperRequestHandler<TUnitOfWork, TRequest, TResponse> : IRunnable<TUnitOfWork, TResponse>
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
