namespace IsolatedSetup.Core.Interfaces;

public interface IExampleWriteStore
{
    Task SetLastMessage(string message, CancellationToken cancellationToken);
}

public interface IExampleReadStore
{
    Task<string?> GetLastMessage(CancellationToken cancellationToken);
}
