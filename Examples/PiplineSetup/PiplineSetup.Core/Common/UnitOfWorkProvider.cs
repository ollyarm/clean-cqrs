using CleanCQRS;

namespace PiplineSetup.Core.Common;

using Interfaces;

public class UnitOfWorkProvider : IUnitOfWorkProvider
{
    private readonly ICQRSRequestHandlerProvider _createRequestHandlerProvider;
    private readonly IClock _clock;

    public UnitOfWorkProvider(ICQRSRequestHandlerProvider createRequestHandlerProvider, IClock clock)
    {
        _createRequestHandlerProvider = createRequestHandlerProvider;
        _clock = clock;
    }

    public IUnitOfWork Start() => new UnitOfWork(_createRequestHandlerProvider.GetRequestHandler<IUnitOfWork>(), _clock);
}
