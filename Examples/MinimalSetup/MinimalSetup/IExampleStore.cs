namespace MinimalSetup;

public interface IExampleStore
{
    void SetLastMessage(string message);
    string? GetLastMessage();
}
