using UnityEngine;
using Zenject;
using Core.ECS;
using Core.Models;
using Core.Services;
using Core.ECS.Behaviours;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Object _geoPrefab;

        public override void InstallBindings()
        {        
            Container.BindInterfacesTo<ECSStartup>().AsSingle().NonLazy();

            BindPools();
            BindPlayer();
            BindUnits();
        }

        private void BindUnits()
        {
            Container.Bind<UnitsModelsProvider>().AsSingle();
        }
        private void BindPlayer()
        {
            Container.BindInterfacesTo<StandaloneInput>().AsSingle();
        }
        private void BindPools()
        {
            Container.BindFactory<ushort, GeoView, GeoView.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(30)
                .FromComponentInNewPrefab(_geoPrefab)
                .UnderTransformGroup("Geo"));
        }
    }
}