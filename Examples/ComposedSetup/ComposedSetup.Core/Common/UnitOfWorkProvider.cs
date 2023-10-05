using CleanCQRS;

namespace ComposedSetup.Core.Common;

using Interfaces;

public class UnitOfWorkProvider : IUnitOfWorkProvider
{
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUnitOfWork Start() => new UnitOfWork(_serviceProvider);
}
