using System;

namespace Core.Services
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
        float InGameTime { get; }
        DateTime UtcNow { get; }
    }
}