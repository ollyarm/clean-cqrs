using CleanCQRS;

namespace SimpleSetup.Core.Examples;

using Common;
using Interfaces;


public class ExampleQuery : IQuery<string>
{
    public class Handler : SyncQueryHandler<ExampleQuery, string>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override string Run(IUnitOfWork uow, ExampleQuery query) => _exampleStore.GetLastMessage() ?? "";
    }
}
