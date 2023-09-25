namespace CleanCQRS;

public interface IUnitOfWorkCommand
{
    Task Run(ICommand command, CancellationToken cancellationToken);
    Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken);
}
