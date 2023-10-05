namespace PiplineSetup.Web;

using Core.Common;
using Core.Interfaces;

public class ExampleStore : IExampleStore
{
    private string? _message;
    private TimeSpan _delay;

    public ExampleStore(TimeSpan delay)
    {
        _delay = delay;
    }
    public Task<string?> GetLastMessage(CancellationToken cancellationToken) 
        => Task.Delay(_delay).Then(() => _message);

    public Task SetLastMessage(string message, CancellationToken cancellationToken) 
        => Task.Delay(_delay).Then(() => _message = message);
}
