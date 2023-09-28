using CleanCQRS;

namespace PiplineSetup.Core.Common;

using Interfaces;

public class UnitOfWorkProvider : IUnitOfWorkProvider
{
    private readonly Func<ICQRSRequestHandler> _createRequestHandler;
    private readonly IClock _clock;

    public UnitOfWorkProvider(Func<ICQRSRequestHandler> createRequestHandler, IClock clock)
    {
        _createRequestHandler = createRequestHandler;
        _clock = clock;
    }

    public IUnitOfWork Start() => new UnitOfWork(_createRequestHandler(), _clock);
}
