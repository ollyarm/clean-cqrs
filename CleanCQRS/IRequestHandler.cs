namespace CleanCQRS;

public interface IRequestHandler<TUnitOfWork, TRequest, TResponse>
{
    Task<TResponse> Run(TUnitOfWork uow, TRequest request, CancellationToken cancellationToken);
}
