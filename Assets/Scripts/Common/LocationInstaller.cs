using Core.Units;
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
            Container.Bind<UnitScript>()
                .FromComponentInNewPrefab(Prefab)
                .AsSingle()
                .OnInstantiated<UnitScript>(OnPlayerInstantiated);
        }

        private void OnPlayerInstantiated(InjectContext context, UnitScript player)
        {
            player.transform.position = LocationPoint.position;
            Debug.Log("Player Instantiated!");
        }
    }
}