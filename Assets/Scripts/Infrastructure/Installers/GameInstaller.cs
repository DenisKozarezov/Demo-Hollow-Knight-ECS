using Core.ECS;
using Core.Input;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Object _geoPrefab; 

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StandaloneInput>().AsSingle();
            Container.BindInterfacesTo<ECSStartup>().AsSingle();

            BindPools();
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