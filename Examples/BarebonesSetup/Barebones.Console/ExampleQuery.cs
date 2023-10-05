using CleanCQRS;

namespace Barebones.Console;
public class ExampleQuery : IQuery<int>
{
    public class Handler : IRequestHandler<ICQRSRequestHandler, ExampleQuery, int>
    {
        private readonly ExampleStore exampleStore;

        public Handler(ExampleStore exampleStore)
        {
            this.exampleStore = exampleStore;
        }

        public Task<int> Run(ICQRSRequestHandler uow, ExampleQuery request, CancellationToken cancellationToken)
            => Task.FromResult(exampleStore.GetCount() ?? 0);
    }
}