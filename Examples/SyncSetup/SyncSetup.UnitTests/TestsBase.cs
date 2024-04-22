using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;

namespace SyncSetup.UnitTests;

public abstract class TestsBase
{
    private static ServiceProvider _serviceProvider = null!;// Set in OneTime Setup 
    private TestStore _store;
    private IUnitOfWork _uow;

    [OneTimeSetUp]
    public static void OneTimeSetup()
    {
        _serviceProvider = new ServiceCollection()
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
        _serviceProvider?.Dispose();
        _serviceProvider = null!;
    }


    [SetUp]
    public void Setup()
    {
        //create a scoped provider to isolate each test
        var provider = _serviceProvider.CreateScope().ServiceProvider;
        _store = provider.GetRequiredService<TestStore>();
        _uow = provider.GetRequiredService<IUnitOfWork>();
    }

    [TearDown]
    public void TearDown()
    {
        _uow?.Dispose();
        _uow = null!;
    }

    protected TestStore Store => _store;
    protected T Run<T>(ICommand<T> command) => _uow.Run(command);
    protected void Run(ICommand command) => _uow.Run(command);
    protected T Run<T>(IQuery<T> query) => _uow.Run(query);
}
