using Newtonsoft.Json.Linq;

namespace Core.Serialization
{
    internal interface ISerializableObject
    {
        JObject Serialize();
    }
}