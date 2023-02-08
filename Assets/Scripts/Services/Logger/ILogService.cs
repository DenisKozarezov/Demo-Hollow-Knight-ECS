namespace Core.Services
{
    public interface ILogService
    {
        void Log(string message);
        void LogFormat(string message, params object[] args);
    }
}
