namespace Core.Services
{
    public sealed class GameIdentifierRegistry : IIdentifierService
    {
        // Temp
        private int _lastID = -1;

        public int Next() => ++_lastID;
    }
}
