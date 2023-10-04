namespace CleanCQRS.Handlers;

public abstract class SyncQueryHandlerBase<TUnitOfWork, TQuery, TResult> 
    : IRequestHandler<TUnitOfWork, TQuery, TResult> where TQuery : IQuery<TResult>
{

    protected abstract TResult Run(TUnitOfWork uow, TQuery query);

    Task<TResult> IRequestHandler<TUnitOfWork, TQuery, TResult>.Run(TUnitOfWork uow, TQuery request, CancellationToken cancellationToken) 
        => Task.FromResult(Run(uow, request));
}
