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
            Container.Bind<PlayerController>()
                .FromComponentInNewPrefab(Prefab)
                .AsSingle()
                .NonLazy();

            PlayerController playerController = Container.Resolve<PlayerController>();           
        }
    }
}