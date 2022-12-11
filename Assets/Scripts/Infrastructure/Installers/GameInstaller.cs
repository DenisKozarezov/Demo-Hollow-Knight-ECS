using UnityEngine;
using Zenject;
using Core.ECS;
using Core.Input;
using Core.Models;
using Core.UI;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Object _geoPrefab;
        [SerializeField]
        private HealthUIView _healthUIView;
        [SerializeField]
        private GeoUIView _geoUIView;

        public override void InstallBindings()
        {        
            Container.Bind<ICoroutineRunner>().To<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesTo<ECSStartup>().AsSingle();

            BindPools();
            BindPlayer();
            BindUnits();
            BindUI();
        }

        private void BindUnits()
        {
            Container.Bind<UnitsModelsProvider>().AsSingle();
        }
        private void BindPlayer()
        {
            Container.BindInterfacesTo<StandaloneInput>().AsSingle();
        }
        private void BindUI()
        {
            Container.Bind<GeoUIView>().FromInstance(_geoUIView).AsSingle();
            Container.Bind<HealthUIView>().FromInstance(_healthUIView).AsSingle();
        }
        private void BindPools()
        {
            Container.BindFactory<GeoView, GeoView.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(30)
                .FromComponentInNewPrefab(_geoPrefab)
                .UnderTransformGroup("Geo"));
        }
    }
}