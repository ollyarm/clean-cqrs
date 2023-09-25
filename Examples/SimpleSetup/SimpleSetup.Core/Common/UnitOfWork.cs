using CleanCQRS;

namespace SimpleSetup.Core.Common;

using Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private ICQRSRequestHandler _requestHandler;

    public UnitOfWork(ICQRSRequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public void Dispose() { }

    public Task Run(ICommand command, CancellationToken cancellationToken) => _requestHandler.HandleCommand<IUnitOfWork>(this, command, cancellationToken);

    public Task<T> Run<T>(ICommand<T> command, CancellationToken cancellationToken) => _requestHandler.HandleCommand<IUnitOfWork, T>(this, command, cancellationToken);

    public Task<T> Run<T>(IQuery<T> query, CancellationToken cancellationToken) => _requestHandler.HandleQuery<IUnitOfWork, T>(this, query, cancellationToken);
}