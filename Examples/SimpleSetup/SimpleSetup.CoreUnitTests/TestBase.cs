global using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSetup.CoreUnitTests;

using Core;
using Core.Interfaces;
using Fakes;

public abstract class TestBase
{
    protected FakeExampleStore FakeExampleStore { get; private set; }
    protected IUnitOfWork Uow { get; private set; }
    private ServiceProvider ServiceProvider { get; set; }

    [OneTimeSetUp] 
    public void OneTimeSetUp() 
    {
        ServiceProvider = new ServiceCollection()
            .AddScoped<FakeExampleStore>()
            .AddScoped<IExampleStore>(sp => sp.GetRequiredService<FakeExampleStore>())
            .AddScoped<IExampleAsyncStore>(sp => sp.GetRequiredService<FakeExampleStore>())
            .AddCore()
            .BuildServiceProvider()
            ;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ServiceProvider?.Dispose();
        ServiceProvider = null!;
    }


    [SetUp]
    public void Setup()
    {
        var serviceProvider = ServiceProvider.CreateScope().ServiceProvider;

        FakeExampleStore = serviceProvider.GetRequiredService<FakeExampleStore>();
        Uow = serviceProvider.GetRequiredService<IUnitOfWorkProvider>().Start();

    }

    [TearDown]
    public void TearDown()
    {
        Uow?.Dispose();
        Uow = null!;
    }
}