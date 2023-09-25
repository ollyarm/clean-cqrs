using CleanCQRS;

namespace SimpleSetup.Core.Examples;

using Common;
using Interfaces;

public class ExampleCommandWithAsyncDependency : ICommand
{
    public string? Message { get; set; }

    public class Handler : AsyncCommandHandler<ExampleCommandWithAsyncDependency>
    {
        private readonly IExampleAsyncStore _exampleStore;

        public Handler(IExampleAsyncStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override Task Run(IUnitOfWork uow, ExampleCommandWithAsyncDependency command, CancellationToken cancellationToken)
             => _exampleStore.SetLastMessage($"Last run with async message: '{command.Message.PrepareMessage()}'", cancellationToken);
    }
}
