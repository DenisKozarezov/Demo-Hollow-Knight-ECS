using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using Core.Serializable;

namespace Core.Models
{
    enum ScreenResolution : byte
    {
        _800x600 = 0,
        _1024x768 = 1,
        _1280x720 = 2,
        _1280x800 = 4,

    }

    [CreateAssetMenu(menuName = "Configuration/Settings/Create GraphicsSettings")]
    public class GraphicsSettings : ScriptableObject, ISerializableObject
    {
        [Header("Settings")]
        [SerializeField]
        private ScreenResolution _screenResolution;

        public JObject Serialize()
        {
            JObject obj = new JObject();
            obj.Add("resolution:", (byte)_screenResolution);
            return obj;
        }
    }
}