namespace IsolatedSetup.Web;

using Core.Common;
using Core.Interfaces;

public class ExampleStore : IExampleReadStore, IExampleWriteStore
{
    private string? _message;

    public Task<string?> GetLastMessage(CancellationToken cancellationToken) => _message.AsTask();

    public Task SetLastMessage(string message, CancellationToken cancellationToken) => (_message = message).AsTask();
}
