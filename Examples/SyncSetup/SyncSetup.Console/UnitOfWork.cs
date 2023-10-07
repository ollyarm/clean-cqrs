using CleanCQRS;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler _requestHandler;

    public UnitOfWork(ICQRSRequestHandler requestHandler, IExampleStore exampleStore)
    {
        _requestHandler = requestHandler;
        Store = exampleStore;
    }

    public IExampleStore Store { get; }

    public void Dispose() { }

    public void Run(ICommand command) => _requestHandler.HandleCommand<IUnitOfWork>(this, command, CancellationToken.None).GetAwaiter().GetResult();

    public T Run<T>(ICommand<T> command) => _requestHandler.HandleCommand<IUnitOfWork, T>(this, command, CancellationToken.None).GetAwaiter().GetResult();

    public T Run<T>(IQuery<T> query) => _requestHandler.HandleQuery<IUnitOfWork, T>(this, query, CancellationToken.None).GetAwaiter().GetResult();
}