using CleanCQRS;

namespace MinimalSetup;

public interface IUnitOfWork : IDisposable
{
    Task Run(ICommand command);
    Task<T> Run<T>(ICommand<T> command);
    Task<T> Run<T>(IQuery<T> query);
    
}
