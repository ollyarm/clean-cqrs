namespace MinimalSetup;

// this would usually be in an outer layer and not in core project
public class ExampleStore : IExampleStore
{
    private string? _message;

    public string? GetLastMessage() => _message; 

    public void SetLastMessage(string message) => _message = message;
}