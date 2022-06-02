using UnityEngine;
using Zenject;

namespace Core.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LocationPoint;
        [SerializeField]
        private GameObject Prefab;

        public override void InstallBindings()
        {
            PlayerController playerController = Container.InstantiatePrefabForComponent<PlayerController>(Prefab, LocationPoint.position, Quaternion.identity, null);

            Container.Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle()
                .NonLazy();
        }
    }
}