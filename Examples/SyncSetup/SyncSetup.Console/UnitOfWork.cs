using CleanCQRS;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler<IUnitOfWork> _requestHandler;

    public UnitOfWork(ICQRSRequestHandler<IUnitOfWork> requestHandler, IExampleStore exampleStore)
    {
        _requestHandler = requestHandler;
        Store = exampleStore;
    }

    public IExampleStore Store { get; }

    public void Dispose() { }

    public void Run(ICommand command) => _requestHandler.HandleCommand(this, command, CancellationToken.None).GetAwaiter().GetResult();

    public T Run<T>(ICommand<T> command) => _requestHandler.HandleCommand(this, command, CancellationToken.None).GetAwaiter().GetResult();

    public T Run<T>(IQuery<T> query) => _requestHandler.HandleQuery(this, query, CancellationToken.None).GetAwaiter().GetResult();
}