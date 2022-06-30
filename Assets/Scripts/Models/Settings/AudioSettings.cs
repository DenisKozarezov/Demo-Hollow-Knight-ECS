using UnityEngine;
using Newtonsoft.Json.Linq;
using Core.Serialization;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create Audio Settings")]
    public class AudioSettings : ScriptableObject, ISerializableObject
    {
        [Header("Settings")]
        [SerializeField, Range(0f, 1f)]
        private float _globalVolume;
        [SerializeField, Range(0f, 1f)]
        private float _musicVolume;
        [SerializeField, Range(0f, 1f)]
        private float _environmentVolume;
        [SerializeField, Range(0f, 1f)]
        private float _unitsVolume;
        [SerializeField]
        private bool _isMute;

        public float GlobalVolume => _globalVolume;
        public float MusicVolume => _musicVolume;
        public float EnvironmentVolume => _environmentVolume;
        public float UnitsVolume => _unitsVolume;
        public bool IsMute => _isMute;

        public JObject Serialize()
        {
            JObject obj = new JObject();
            obj.Add("global", _globalVolume);
            obj.Add("music", _musicVolume);
            obj.Add("environment", _environmentVolume);
            obj.Add("units", _unitsVolume);
            obj.Add("mute", _isMute);
            return obj;
        }
    }
}