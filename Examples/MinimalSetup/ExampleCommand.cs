using CleanCQRS;

namespace MinimalSetup;

public class ExampleCommand : ICommand
{
    public string Message { get; set; } = "";

    public class Handler : IRequestHandler<IUnitOfWork, ExampleCommand, EmptyResult>
    {
        private readonly IExampleStore _exampleStore;

        public Handler(IExampleStore exampleDependency)
        {
            _exampleStore = exampleDependency;
        }

        public Task<EmptyResult> Run(IUnitOfWork uow, ExampleCommand request, CancellationToken cancellationToken)
        {
            var message = string.IsNullOrWhiteSpace(request.Message) ? "No message provided" : request.Message;
            _exampleStore.SetLastMessage($"Last run with message: '{message}'");
            return Task.FromResult(EmptyResult.Value);
        }
    }
}