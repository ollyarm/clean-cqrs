using Barebones.Console;
using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ExampleStore>()
    .AddCleanCQRS(Project.Assembly)
    .BuildServiceProvider()
    ;

var runner = serviceProvider.GetRequiredService<ICQRSRequestHandler>();

var command = new ExampleCommand()
{
    RunTill = 100
};

var message = await runner.HandleCommand(runner, command, CancellationToken.None);

Console.WriteLine(message);

public static class Project
{
    public static Assembly Assembly => typeof(Project).Assembly;
}
