using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using MinimalSetup;
using System.Reflection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IExampleStore, ExampleStore>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddCleanCQRS(Project.Assembly)
    .BuildServiceProvider()
    ;

Console.WriteLine("Please enter a message");

var input = Console.ReadLine() ?? "";

using (var uow = serviceProvider.GetRequiredService<IUnitOfWork>())
{
    var command = new ExampleCommand()
    {
        Message = input,
    };

    await uow.Run(command);

    var query = new ExampleQuery();

    var result = await uow.Run(query);

    Console.WriteLine(result);
}

public static class Project
{
    public static Assembly Assembly => typeof(Project).Assembly;
}