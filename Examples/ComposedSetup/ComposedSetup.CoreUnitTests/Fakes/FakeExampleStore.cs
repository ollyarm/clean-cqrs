namespace ComposedSetup.CoreUnitTests.Fakes;

using Core.Interfaces;
using Core.Common;

public class FakeExampleStore : IExampleStore
{
    public string? LastMessage = "Test Message";

    Task<string?> IExampleStore.GetLastMessage(CancellationToken cancellationToken) => LastMessage.AsTask();

    Task IExampleStore.SetLastMessage(string message, CancellationToken cancellationToken) => (LastMessage = message).AsTask();
}
