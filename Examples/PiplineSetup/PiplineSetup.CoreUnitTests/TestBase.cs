global using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace PiplineSetup.CoreUnitTests;

using Core;
using Core.Interfaces;
using Fakes;
using Microsoft.Extensions.Logging;

public abstract class TestBase
{
    protected FakeExampleStore FakeExampleStore { get; private set; }
    protected FakeClock Clock { get; private set; }
    protected IUnitOfWork Uow { get; private set; }
    private ServiceProvider ServiceProvider { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ServiceProvider = new ServiceCollection()
            .AddLogging(x => x.AddConsole())
            .AddScopedTestImplimentation<IExampleStore, FakeExampleStore>()
            .AddScopedTestImplimentation<IClock, FakeClock>()
            .AddCore()
            .BuildServiceProvider()
            ;
    }

    [SetUp]
    public void Setup()
    {
        var serviceProvider = ServiceProvider.CreateScope().ServiceProvider;

        FakeExampleStore = serviceProvider.GetRequiredService<FakeExampleStore>();
        Uow = serviceProvider.GetRequiredService<IUnitOfWorkProvider>().Start();
        Clock = serviceProvider.GetRequiredService<FakeClock>();
    }
}

public static class TestBaseHelpers
{
    public static IServiceCollection AddScopedTestImplimentation<TInterface, TType>(this IServiceCollection services) 
        where TType : class, TInterface 
        where TInterface : class 
        => services
            .AddScoped<TType>()
            .AddScoped<TInterface>(ctx => ctx.GetRequiredService<TType>())
            ;
}