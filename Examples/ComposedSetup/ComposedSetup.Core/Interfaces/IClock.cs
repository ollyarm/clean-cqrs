namespace ComposedSetup.Core.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}