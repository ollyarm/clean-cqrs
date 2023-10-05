namespace IsolatedSetup.Core.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}