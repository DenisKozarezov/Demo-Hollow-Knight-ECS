using Unity.Plastic.Newtonsoft.Json.Linq;

namespace Core.Serializable
{
    internal interface ISerializableObject
    {
        JObject Serialize();
    }
}