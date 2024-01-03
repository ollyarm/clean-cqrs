using CleanCQRS;

namespace SimpleSetup.Core.Common;

using Interfaces;

public class UnitOfWorkProvider : IUnitOfWorkProvider
{
    private readonly Func<ICQRSRequestHandler> _createRequestHandler;

    public UnitOfWorkProvider(Func<ICQRSRequestHandler> createRequestHandler)
    {
        _createRequestHandler = createRequestHandler;
    }

    public IUnitOfWork Start() => new UnitOfWork(_createRequestHandler());
}
