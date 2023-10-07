namespace SyncSetup.UnitTests;

public class TestStore : IExampleStore
{
    public string? Message { get; set; }

    public string? GetLastMessage() => Message;

    public void SetLastMessage(string message) => Message = message;
}
