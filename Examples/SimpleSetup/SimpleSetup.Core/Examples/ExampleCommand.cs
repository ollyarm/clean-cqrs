using CleanCQRS;

namespace SimpleSetup.Core.Examples;

using Common;
using Interfaces;

public class ExampleCommand : ICommand
{
    public string? Message { get; set; }

    public class Handler : SyncCommandHandler<ExampleCommand>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override void Run(IUnitOfWork uow, ExampleCommand command) => _exampleStore.SetLastMessage($"Last run with message: '{command.Message.PrepareMessage()}'");
    }
}
