using CleanCQRS;

namespace Barebones.Console;

public class IncrementCommand : ICommand
{

    public class Handler : IRequestHandler<ICQRSRequestHandler, IncrementCommand, EmptyResult>
    {
        private readonly ExampleStore exampleStore;

        public Handler(ExampleStore exampleStore)
        {
            this.exampleStore = exampleStore;
        }

        public Task<EmptyResult> Run(ICQRSRequestHandler uow, IncrementCommand request, CancellationToken cancellationToken)
        {
            if (exampleStore.GetCount() == null)
            {
                exampleStore.Init(1);
            }
            else 
            { 
                exampleStore.Increment();
            }
            return EmptyResult.TaskValue;
        }
    }
}
