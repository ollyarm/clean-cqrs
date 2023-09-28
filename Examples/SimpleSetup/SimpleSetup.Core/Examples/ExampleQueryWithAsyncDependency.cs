using CleanCQRS;

namespace SimpleSetup.Core.Examples;

using Common;
using Interfaces;


public class ExampleQueryWithAsyncDependency : IQuery<string>
{
    public class Handler : AsyncQueryHandler<ExampleQueryWithAsyncDependency, string>
    {
        private readonly IExampleAsyncStore _exampleStore;

        public Handler(IExampleAsyncStore exampleStore)
        {
            _exampleStore = exampleStore;
        }

        protected override Task<string> Run(IUnitOfWork uow, ExampleQueryWithAsyncDependency query, CancellationToken cancellationToken) 
            => _exampleStore.GetLastMessage(cancellationToken).Then(message => message ?? "");
    }
}