using CleanCQRS;

public class ShouldContinueCommand : ICommand<ShouldContinueCommand.Result>
{
    public string Input { get; set; } = "";

    public class Handler : CleanCQRS.Handlers.SyncCommandHandlerBase<IUnitOfWork, ShouldContinueCommand, Result>
    {
        protected override Result Run(IUnitOfWork uow, ShouldContinueCommand command)
        {
            switch (command.Input?.ToLower())
            {
                case "y":
                case "yes":
                    return Result.Yes;

                case "n":
                case "no":
                    return Result.No;

                default:
                    return Result.Unknown;
            }
        }
    }

    public enum Result
    {
        Unknown = 0,
        Yes = 1,
        No = 2,
    }
}