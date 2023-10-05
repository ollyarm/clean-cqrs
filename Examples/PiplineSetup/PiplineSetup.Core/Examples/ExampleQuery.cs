using CleanCQRS;

namespace PiplineSetup.Core.Examples;

using Common;
using Interfaces;


public class ExampleQuery : IQuery<string>
{
    public class Handler : AsyncQueryHandler<ExampleQuery, string>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override Task<string> Run(IUnitOfWork uow, ExampleQuery query, CancellationToken cancellationToken) =>
            _exampleStore
                .GetLastMessage(cancellationToken)
                .Then(message => message ?? "")
                ;
    }
}
