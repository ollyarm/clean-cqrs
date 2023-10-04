using MinimalSetup;

namespace MinimalSetupTests.Fakes;

// this is a fake dependency (would usually be a fake for interface whos instance is in an outer layer and not in core project)
public class TestStore : IExampleStore
{
    public string LastMessage { get; set; } = "Test Message";

    string IExampleStore.GetLastMessage() => LastMessage;

    void IExampleStore.SetLastMessage(string message) => LastMessage = message;
}