using CleanCQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IExampleStore, ExampleStore>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddCleanCQRS(Project.Assembly)
    .BuildServiceProvider()
    ;


var @continue = true;

while(@continue)
{
    Console.WriteLine("Please enter a message");
    var input = Console.ReadLine() ?? "";

    using(var uow = serviceProvider.GetRequiredService<IUnitOfWork>())
    {
        uow.Run(new ExampleCommand() { Input = input });
        Console.WriteLine(uow.Run(new ExampleQuery()));
        var shouldContinue = ShouldContinueCommand.Result.Unknown;
        var shouldConinuteMessage = "Carry on?";
        while (shouldContinue == ShouldContinueCommand.Result.Unknown)
        {
            Console.WriteLine(shouldConinuteMessage);
            shouldContinue = uow.Run(new ShouldContinueCommand() { Input = Console.ReadLine() ?? "" });
            shouldConinuteMessage = "Sorry, please enter Yes(Y) or No(N)...";
        }
        @continue = shouldContinue == ShouldContinueCommand.Result.Yes;
    }
}


public static class Project
{
    public static Assembly Assembly => typeof(Project).Assembly;
}
