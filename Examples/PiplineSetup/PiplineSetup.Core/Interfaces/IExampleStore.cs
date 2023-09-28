namespace PiplineSetup.Core.Interfaces;

public interface IExampleStore
{
    Task SetLastMessage(string message, CancellationToken cancellationToken);
    Task<string?> GetLastMessage(CancellationToken cancellationToken);
}
