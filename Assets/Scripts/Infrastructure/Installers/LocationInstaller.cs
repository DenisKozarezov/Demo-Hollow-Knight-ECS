using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _playerSpawn;
        [SerializeField]
        private Transform _enemySpawn;
        [SerializeField]
        private Object _enemyPrefab;
        [SerializeField]
        private Object _playerPrefab;

        public override void InstallBindings()
        {
            Container.InstantiatePrefab(_playerPrefab, _playerSpawn.transform.position, Quaternion.identity, null);
            Container.InstantiatePrefab(_enemyPrefab, _enemySpawn.transform.position, Quaternion.identity, null);
        }
    }
}