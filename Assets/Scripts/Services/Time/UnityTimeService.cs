using System;
using UnityEngine;

namespace Core.Services
{
    public sealed class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
        public float InGameTime => Time.time;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
