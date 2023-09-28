namespace CleanCQRS;

public interface IRequestHandler<in TUnitOfWork, in TRequest, TResponse>
{
    Task<TResponse> Run(TUnitOfWork uow, TRequest request, CancellationToken cancellationToken);
}
