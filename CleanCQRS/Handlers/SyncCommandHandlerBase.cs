namespace CleanCQRS.Handlers;

public abstract class SyncCommandHandlerBase<TUnitOfWork, TCommand, TResult>
    : IRequestHandler<TUnitOfWork, TCommand, TResult> where TCommand : ICommand<TResult>
{

    protected abstract TResult Run(TUnitOfWork uow, TCommand command);

    Task<TResult> IRequestHandler<TUnitOfWork, TCommand, TResult>.Run(TUnitOfWork uow, TCommand request, CancellationToken cancellationToken)
        => Task.FromResult(Run(uow, request));
}

public abstract class SyncCommandHandlerBase<TUnitOfWork, TCommand>
    : IRequestHandler<TUnitOfWork, TCommand, EmptyResult> where TCommand : ICommand
{

    protected abstract void Run(TUnitOfWork uow, TCommand command);

    Task<EmptyResult> IRequestHandler<TUnitOfWork, TCommand, EmptyResult>.Run(TUnitOfWork uow, TCommand request, CancellationToken cancellationToken)
    {
        Run(uow, request);
        return Task.FromResult(EmptyResult.Value);
    }
}