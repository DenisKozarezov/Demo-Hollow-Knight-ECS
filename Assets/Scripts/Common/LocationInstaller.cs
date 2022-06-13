using UnityEngine;
using Core.Models;
using Core.Units;
using Zenject;
using Core.Input;

namespace Core.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LocationPoint;
        [Inject]
        private UnitsDefinitions unitsDefinitions;

        public override void Start()
        {
            Container.Resolve<UnitScript>();
        }

        public override void InstallBindings()
        {
            Container
                .Bind<IInputSystem>()
                .FromResolveGetter<UnitScript>(x => x.GetComponent<IInputSystem>())
                .AsSingle(); 
            Container.Bind<UnitScript>()
                .FromComponentInNewPrefabResource(unitsDefinitions.PlayerModel.PrefabPath)
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