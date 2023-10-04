namespace CleanCQRS.Handlers;

public abstract class AsyncQueryHandlerBase<TUnitOfWork, TQuery, TResult>
    : IRequestHandler<TUnitOfWork, TQuery, TResult> where TQuery : IQuery<TResult>
{
    protected abstract Task<TResult> Run(TUnitOfWork uow, TQuery query, CancellationToken cancellationToken);

    Task<TResult> IRequestHandler<TUnitOfWork, TQuery, TResult>.Run(TUnitOfWork uow, TQuery request, CancellationToken cancellationToken)
        => Run(uow, request, cancellationToken);
}
