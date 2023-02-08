using UnityEngine;
using Core.Services;

namespace Core.Services
{
    public sealed class UnityDebugLogService : ILogService
    {
        public void Log(string message) => Debug.Log(message);
        public void LogFormat(string message, params object[] args) => Debug.LogFormat(message, args);
    }
}
