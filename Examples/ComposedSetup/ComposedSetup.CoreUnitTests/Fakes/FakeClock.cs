namespace ComposedSetup.CoreUnitTests.Fakes;

using Core.Interfaces;

public class FakeClock : IClock
{
    private DateTimeOffset _now = DateTimeOffset.UtcNow;
    
    public DateTimeOffset Set(DateTimeOffset now)
    {
        _now = now;
        return now;
    }

    public DateTimeOffset Set(string dateTime)
    {
        _now = DateTimeOffset.Parse(dateTime);
        return _now;
    }

    DateTime IClock.UtcNow => _now.DateTime.ToUniversalTime();
}
