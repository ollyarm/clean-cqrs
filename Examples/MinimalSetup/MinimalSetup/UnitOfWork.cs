using CleanCQRS;

namespace MinimalSetup;

public class UnitOfWork : IUnitOfWork
{
    private readonly ICQRSRequestHandler<IUnitOfWork> requestHandler;

    public UnitOfWork(ICQRSRequestHandler<IUnitOfWork> requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public void Dispose() { }

    public Task Run(ICommand command) => requestHandler.HandleCommand(this, command, CancellationToken.None);
    public Task<T> Run<T>(ICommand<T> command) => requestHandler.HandleCommand(this, command, CancellationToken.None);
    public Task<T> Run<T>(IQuery<T> query) => requestHandler.HandleQuery(this, query, CancellationToken.None);
}