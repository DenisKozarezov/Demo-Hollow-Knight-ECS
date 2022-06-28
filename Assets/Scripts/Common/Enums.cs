namespace Core
{
    public enum FadeMode : byte { On, Off }
    public enum InteractType : byte
    {
        None = 0x00,
        Read = 0x01,
        Rest = 0x02,
    }
    public enum ItemType : byte
    {
        None = 0x01,
        Geo = 0x02,
        Spell = 0x03
    }
}
