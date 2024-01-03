using Microsoft.Extensions.DependencyInjection;

namespace CleanCQRS.Handlers;

public class CQRSRequestHandlerProvider : ICQRSRequestHandlerProvider
{
    private readonly IServiceProvider _serviceProvider;
    public CQRSRequestHandlerProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICQRSRequestHandler GetRequestHandler() =>  _serviceProvider.GetRequiredService<ICQRSRequestHandler>();
    public ICQRSRequestHandler<T> GetRequestHandler<T>() =>  _serviceProvider.GetRequiredService<ICQRSRequestHandler<T>>();
}
