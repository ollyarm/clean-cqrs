namespace CleanCQRS;

public interface IUnitOfWorkCommandWithoutCancellationToken
{
    Task Run(ICommand command);
    Task<T> Run<T>(ICommand<T> command);
}
