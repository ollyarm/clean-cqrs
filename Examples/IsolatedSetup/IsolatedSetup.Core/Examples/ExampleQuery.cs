using CleanCQRS;

namespace IsolatedSetup.Core.Examples;

using Common;
using Interfaces;


public class ExampleQuery : IQuery<string>
{
    public class Handler : AsyncQueryHandler<ExampleQuery, string>
    {
        protected override Task<string> Run(IQueryUnitOfWork uow, ExampleQuery query, CancellationToken cancellationToken) =>
            uow.ExampleReadStore
                .GetLastMessage(cancellationToken)
                .Then(message => message ?? "")
                ;
    }
}
