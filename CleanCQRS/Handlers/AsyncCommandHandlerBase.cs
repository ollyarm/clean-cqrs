namespace CleanCQRS.Handlers;

public abstract class AsyncCommandHandlerBase<TUnitOfWork, TCommand, TResult>
    : IRequestHandler<TUnitOfWork, TCommand, TResult> where TCommand : ICommand<TResult>
{
    protected abstract Task<TResult> Run(TUnitOfWork uow, TCommand command, CancellationToken cancellationToken);

    Task<TResult> IRequestHandler<TUnitOfWork, TCommand, TResult>.Run(TUnitOfWork uow, TCommand request, CancellationToken cancellationToken)
        => Run(uow, request, cancellationToken);
}

public abstract class AsyncCommandHandlerBase<TUnitOfWork, TCommand>
    : IRequestHandler<TUnitOfWork, TCommand, EmptyResult> where TCommand : ICommand
{
    protected abstract Task Run(TUnitOfWork uow, TCommand command, CancellationToken cancellationToken);

    async Task<EmptyResult> IRequestHandler<TUnitOfWork, TCommand, EmptyResult>.Run(TUnitOfWork uow, TCommand request, CancellationToken cancellationToken)
    {
        await Run(uow, request, cancellationToken);
        return EmptyResult.Value;
    }
}
