using UnityEngine;
using Newtonsoft.Json.Linq;
using Zenject;
using Core.Serializable;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create GameSettings")]
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
            obj.Add(_audioSettings.Serialize());
            obj.Add(_graphicsSettings.Serialize());
            return obj;
        }
    }
}