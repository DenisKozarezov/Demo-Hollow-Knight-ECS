using UnityEngine;
using Core.Models;
using Core.Units;
using Zenject;

namespace Core.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LocationPoint;
        [Inject]
        private PlayerModel _playerModel;

        public override void Start()
        {
            Container.Resolve<UnitScript>();
        }

        public override void InstallBindings()
        {
            Container.Bind<UnitScript>()
                .FromComponentInNewPrefabResource(_playerModel.PrefabPath)
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