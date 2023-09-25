namespace CleanCQRS;

public interface IUnitOfWorkQueryWithoutCancelationToken
{
    Task<T> Run<T>(IQuery<T> query);

}
