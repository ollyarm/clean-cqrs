using CleanCQRS;

namespace MinimalSetup;

public class ExampleQuery : IQuery<string>
{

    public class Handler : IRequestHandler<IUnitOfWork, ExampleQuery, string>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleDependency)
        {
            _exampleStore = exampleDependency;
        }

        public Task<string> Run(IUnitOfWork uow, ExampleQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_exampleStore.GetLastMessage() ?? "");
        }
    }
}