namespace SimpleSetup.CoreUnitTests.Fakes;

using Core.Interfaces;
using Core.Common;

public class FakeExampleStore : IExampleStore, IExampleAsyncStore
{
    public string? LastMessage = "Test Message";

    string? IExampleStore.GetLastMessage() => LastMessage;
    
    void IExampleStore.SetLastMessage(string message) => LastMessage = message;

    Task<string?> IExampleAsyncStore.GetLastMessage(CancellationToken cancellationToken) => LastMessage.AsTask();

    Task IExampleAsyncStore.SetLastMessage(string message, CancellationToken cancellationToken) => (LastMessage = message).AsTask();
}
