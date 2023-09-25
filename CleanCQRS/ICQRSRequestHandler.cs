namespace CleanCQRS;

public interface ICQRSRequestHandler
{
    Task HandleCommand<TUnitOfWork>(TUnitOfWork uow, ICommand command, CancellationToken cancellationToken);
    Task<TResponse> HandleCommand<TUnitOfWork, TResponse>(TUnitOfWork uow, ICommand<TResponse> command, CancellationToken cancellationToken);
    Task<TResponse> HandleQuery<TUnitOfWork, TResponse>(TUnitOfWork uow, IQuery<TResponse> query, CancellationToken cancellationToken);
}