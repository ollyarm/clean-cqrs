using CleanCQRS;

namespace MinimalSetup;

public class UnitOfWork : IUnitOfWork
{
    private readonly ICQRSRequestHandler requestHandler;

    public UnitOfWork(ICQRSRequestHandler requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public void Dispose() { }

    public Task Run(ICommand command) => requestHandler.HandleCommand<IUnitOfWork>(this, command, CancellationToken.None);
    public Task<T> Run<T>(ICommand<T> command) => requestHandler.HandleCommand<IUnitOfWork, T>(this, command, CancellationToken.None);
    public Task<T> Run<T>(IQuery<T> query) => requestHandler.HandleQuery<IUnitOfWork, T>(this, query, CancellationToken.None);
}