namespace IsolatedSetup.CoreUnitTests.Fakes;

using Core.Interfaces;
using Core.Common;

public class FakeExampleStore : IExampleReadStore, IExampleWriteStore
{
    public string? LastMessage = "Test Message";

    Task<string?> IExampleReadStore.GetLastMessage(CancellationToken cancellationToken) => LastMessage.AsTask();

    Task IExampleWriteStore.SetLastMessage(string message, CancellationToken cancellationToken) => (LastMessage = message).AsTask();
}
