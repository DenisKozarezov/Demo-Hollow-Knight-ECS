using UnityEngine;
using Newtonsoft.Json.Linq;
using Zenject;
using Core.Serialization;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller, ISerializableObject
    {
        [Header("Settings")]
        [SerializeField]
        private AudioSettings _audioSettings;
        [SerializeField]
        private GraphicsSettings _graphicsSettings;
        [SerializeField]
        private UnitsDefinitions _unitsDefinitions;
        // other settings...

        public override void InstallBindings()
        {
            Container.BindInstances(_audioSettings, _graphicsSettings, _unitsDefinitions);
        }

        public JObject Serialize()
        {
            JObject obj = new JObject();
            obj.Add("audioSettings", _audioSettings.Serialize());
            obj.Add("graphicsSettings", _graphicsSettings.Serialize());
            return obj;
        }
    }
}