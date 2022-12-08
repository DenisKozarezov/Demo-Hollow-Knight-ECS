using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LocationPoint;
        [SerializeField]
        private Object _playerPrefab;

        public override void InstallBindings()
        {
            Container.InstantiatePrefab(_playerPrefab, LocationPoint.transform.position, Quaternion.identity, null);            
        }
    }
}