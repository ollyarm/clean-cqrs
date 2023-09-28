using CleanCQRS;

namespace PiplineSetup.Core.Examples;

using Common;
using Interfaces;
using PiplineSetup.Core.Exceptions;

public class ExampleCommand : ICommand
{
    public string? Message { get; set; }

    public class Handler : AsyncCommandHandler<ExampleCommand>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override Task Run(IUnitOfWork uow, ExampleCommand command, CancellationToken cancellationToken)
        {
            if("Bad".Equals(command.Message, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new BadRequestException($"'{command.Message}' is not permitted.");
            }
            return _exampleStore.SetLastMessage($"Last run with message: '{command.Message.PrepareMessage()}'", cancellationToken);
        }
    }
}
