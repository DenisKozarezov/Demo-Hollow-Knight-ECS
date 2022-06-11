using UnityEngine;
using Zenject;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [Header("Settings")]
        [SerializeField]
        private PlayerModel _playerModel;
        [SerializeField]
        private AudioSettings _audioSettings;
        [SerializeField]
        private GraphicsSettings _graphicsSettings;
        // other settings...

        public override void InstallBindings()
        {
            Container.BindInstances(_playerModel, _audioSettings, _graphicsSettings);
        }
    }
}