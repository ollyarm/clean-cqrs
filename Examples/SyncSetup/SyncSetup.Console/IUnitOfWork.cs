using CleanCQRS;

public interface IUnitOfWork : IDisposable
{
    void Run(ICommand command);
    T Run<T>(ICommand<T> command);
    T Run<T>(IQuery<T> query);

    IExampleStore Store { get; }

}
