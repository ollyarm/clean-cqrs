namespace CleanCQRS;

public interface IPipeline<TUnitOfWork, TRequest, TResponse>
{
    Task<TResponse> Run(TUnitOfWork uow, TRequest request, IRequestHandler<TUnitOfWork, TRequest, TResponse> handler, CancellationToken cancellationToken);
}
