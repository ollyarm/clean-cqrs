namespace PiplineSetup.Core.Common;

using Interfaces;
using System;

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
