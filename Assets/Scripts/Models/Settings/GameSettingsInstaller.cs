using UnityEngine;
using Zenject;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
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
    }
}