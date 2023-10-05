namespace SimpleSetup.Core.Interfaces;

public interface IExampleAsyncStore
{
    Task SetLastMessage(string message, CancellationToken cancellationToken);
    Task<string?> GetLastMessage(CancellationToken cancellationToken);
}
