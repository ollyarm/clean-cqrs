using CleanCQRS;
using CleanCQRS.Handlers;

public class ExampleCommand : ICommand
{
    public string Input { get; set; } = "";

    public class Handler : SyncCommandHandlerBase<IUnitOfWork, ExampleCommand>
    {
        protected override void Run(IUnitOfWork uow, ExampleCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Input) || "bad".Equals(command.Input, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            uow.Store.SetLastMessage($"Last run with message: '{command.Input}'");
        }
    }
}
