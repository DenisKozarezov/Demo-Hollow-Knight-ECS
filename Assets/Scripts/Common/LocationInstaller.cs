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

        //public override void Start()
        //{
        //    Container.Resolve<PlayerController>();           
        //}

        //public override void InstallBindings()
        //{
        //    Container.Bind<PlayerController>()
        //        .FromComponentInNewPrefab(Prefab)               
        //        .AsSingle() 
        //        .OnInstantiated<PlayerController>(OnPlayerInstantiated);                   
        //}

        //private void OnPlayerInstantiated(InjectContext context, PlayerController player)
        //{
        //    player.transform.position = LocationPoint.position;
        //    Debug.Log("Player Instantiated!");
        //}
    }
}