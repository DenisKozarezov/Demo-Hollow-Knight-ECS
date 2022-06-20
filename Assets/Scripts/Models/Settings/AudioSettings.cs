using UnityEngine;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Core.Serializable;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create AudioSettings")]
    public class AudioSettings : ScriptableObject, ISerializableObject
    {
        [Header("Settings")]
        [SerializeField, Range(0f, 1f)]
        private float _globalVolume;
        [SerializeField, Range(0f, 1f)]
        private float _musicVolume;
        [SerializeField, Range(0f, 1f)]
        private float _environmentVolume;
        [SerializeField]
        private bool _isMute;

        public float GlobalVolume => _globalVolume;
        public float MusicVolume => _musicVolume;
        public float EnvironmentVolume => _environmentVolume;
        public bool IsMute => _isMute;

        public JObject Serialize()
        {
            JObject obj = new JObject();
            obj.Add("global:", _globalVolume);
            obj.Add("music:", _musicVolume);
            obj.Add("environment:", _environmentVolume);
            obj.Add("mute:", _isMute);
            return obj;
        }
    }
}