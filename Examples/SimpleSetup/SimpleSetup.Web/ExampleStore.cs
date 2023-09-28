namespace SimpleSetup.Web;

using Core.Common;
using Core.Interfaces;

public class ExampleStore : IExampleStore, IExampleAsyncStore
{
    private string? _message;

    public string? GetLastMessage() => _message;

    public Task<string?> GetLastMessage(CancellationToken cancellationToken) => _message.AsTask();

    public void SetLastMessage(string message) => _message = message;

    public Task SetLastMessage(string message, CancellationToken cancellationToken) => (_message = message).AsTask();
}
