namespace PiplineSetup.Core.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}