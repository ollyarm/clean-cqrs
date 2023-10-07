public class ExampleStore : IExampleStore
{
    private string? _message;
    public string? GetLastMessage() => _message;
    public void SetLastMessage(string message) => _message = message;
}
