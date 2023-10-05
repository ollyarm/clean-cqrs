using CleanCQRS;

namespace ComposedSetup.Core.Examples;

using Common;
using Interfaces;


public class ExampleQuery : IQuery<string>
{
    public class Handler : AsyncQueryHandler<ExampleQuery, string>
    {
        protected override Task<string> Run(IUnitOfWork uow, ExampleQuery query, CancellationToken cancellationToken) =>
            uow.ExampleStore
                .GetLastMessage(cancellationToken)
                .Then(message => message ?? "")
                ;
    }
}
