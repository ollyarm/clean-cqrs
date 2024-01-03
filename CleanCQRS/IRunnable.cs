namespace CleanCQRS.Handlers;

internal interface IRunnable<TUnitOfWork, TResponse>
{
    Task<TResponse> Run(TUnitOfWork uow, object request, CancellationToken cancellationToken);
}
