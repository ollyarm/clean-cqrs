using CleanCQRS;

namespace Barebones.Console;

public class ExampleCommand : ICommand<string>
{
    public int RunTill { get; set; }
    
    public class Handler : IRequestHandler<ICQRSRequestHandler, ExampleCommand, string>
    {
        public async Task<string> Run(ICQRSRequestHandler uow, ExampleCommand request, CancellationToken cancellationToken)
        {
            var finished = false;
            while (!finished)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await uow.HandleCommand(uow, new IncrementCommand(), cancellationToken);
                var current = await uow.HandleQuery(uow, new ExampleQuery(), cancellationToken);
                finished = current >= request.RunTill;
            }

            return $"Run until finished";
        }
    }
}