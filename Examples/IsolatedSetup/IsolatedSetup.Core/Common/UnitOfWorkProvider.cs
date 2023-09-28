using CleanCQRS;

namespace IsolatedSetup.Core.Common;

using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWorkProvider : IUnitOfWorkProvider
{
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUnitOfWork Start() => new UnitOfWork(
        _serviceProvider.GetRequiredService<ICQRSRequestHandler>(),
        _serviceProvider.GetRequiredService<IClock>(),
        _serviceProvider.GetRequiredService<IMapper>(),
        _serviceProvider.GetRequiredService<IExampleReadStore>(),
        _serviceProvider.GetRequiredService<IExampleWriteStore>()
        );
}
