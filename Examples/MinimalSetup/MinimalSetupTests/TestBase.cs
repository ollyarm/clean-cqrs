global using NUnit.Framework;
using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using MinimalSetup;
using MinimalSetupTests.Fakes;

namespace MinimalSetupTests;

public abstract class TestBase
{
    protected TestStore TestStore { get; private set; }
    protected IUnitOfWork Uow { get; private set; }

    public static ServiceProvider ServiceProvider { get; private set; } = default!; // Set in OneTimeSetUp

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        ServiceProvider = new ServiceCollection()
            .AddScoped<TestStore>()
            .AddScoped<IExampleStore>(sp => sp.GetRequiredService<TestStore>())
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddCleanCQRS(typeof(Project).Assembly)
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
        //create a scoped provider to isolate each test
        var provider = ServiceProvider.CreateScope().ServiceProvider;
        TestStore = provider.GetRequiredService<TestStore>();
        Uow = provider.GetRequiredService<IUnitOfWork>();
    }

    [TearDown]
    public void TearDown()
    {
        Uow?.Dispose();
        Uow = null!;
    }

}
