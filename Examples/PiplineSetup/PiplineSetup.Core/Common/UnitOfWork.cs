using CleanCQRS;

namespace PiplineSetup.Core.Common;

using Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler<IUnitOfWork> _requestHandler;

    public UnitOfWork(ICQRSRequestHandler<IUnitOfWork> requestHandler, IClock clock)
    {
        _requestHandler = requestHandler;
        Clock = clock;
    }

    public IClock Clock { get; }

    public void Dispose() { }

    public Task Run(ICommand command, CancellationToken cancellationToken) => _requestHandler.HandleCommand(this, command, cancellationToken);

    public Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken) => _requestHandler.HandleCommand(this, command, cancellationToken);

    public Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken) => _requestHandler.HandleQuery(this, query, cancellationToken);
}