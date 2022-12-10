using UnityEngine;
using Zenject;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Create Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [Header("Settings")]
        [SerializeField]
        private UnitsDefinitions _unitsDefinitions;

        public override void InstallBindings()
        {
            Container.BindInstances(_unitsDefinitions);
        }
    }
}