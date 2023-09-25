namespace CleanCQRS;

public interface IUnitOfWorkQuery
{ 
    Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken);
}
