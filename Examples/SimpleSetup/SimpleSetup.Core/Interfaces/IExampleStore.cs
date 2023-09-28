namespace SimpleSetup.Core.Interfaces;

public interface IExampleStore
{
    void SetLastMessage(string message);
    string? GetLastMessage();
}
