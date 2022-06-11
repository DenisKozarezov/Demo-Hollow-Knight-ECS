using UnityEngine;
using Zenject;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Create Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private PlayerModel _playerModel;
        // other settings...

        public PlayerModel PlayerModel => _playerModel;

        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().FromScriptableObject(_playerModel).AsSingle();
        }
    }
}