namespace Core
{
    public enum FadeMode : byte { On, Off }
    public enum InteractType : byte
    {
        None = 0x00,
        Read = 0x01,
        Rest = 0x02,
        Talk = 0x04,
        Trade = 0x08
    }
}
