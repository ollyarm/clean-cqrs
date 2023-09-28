using CleanCQRS;

namespace IsolatedSetup.Core.Examples;

using Common;
using Core.Exceptions;
using Interfaces;

public class ExampleCommand : ICommand
{
    public string? Message { get; set; }

    public class Handler : AsyncCommandHandler<ExampleCommand>
    {
        protected override Task Run(ICommandUnitOfWork uow, ExampleCommand command, CancellationToken cancellationToken)
        {
            if("Bad".Equals(command.Message, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new BadRequestException($"'{command.Message}' is not permitted.");
            }
            return uow.ExampleWriteStore.SetLastMessage($"Last run with message: '{command.Message.PrepareMessage()}'", cancellationToken);
        }
    }
}
