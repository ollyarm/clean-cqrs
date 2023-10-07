using CleanCQRS;
using CleanCQRS.Handlers;

public class ExampleQuery : IQuery<string>
{
    public class Handler : SyncQueryHandlerBase<IUnitOfWork, ExampleQuery, string>
    {
        protected override string Run(IUnitOfWork uow, ExampleQuery query) 
            => uow.Store.GetLastMessage() ?? "No message set";
    }
}
