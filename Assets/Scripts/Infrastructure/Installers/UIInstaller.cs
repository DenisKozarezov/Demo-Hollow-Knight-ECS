using UnityEngine;
using Zenject;
using Core.UI;

namespace Core.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private HealthUIView _healthUIView;
        [SerializeField]
        private GeoUIView _geoUIView;
        [SerializeField]
        private GameUIView _gameUIView;

        public override void InstallBindings()
        {
            Container.Bind<GeoUIView>().FromInstance(_geoUIView).AsSingle();
            Container.Bind<HealthUIView>().FromInstance(_healthUIView).AsSingle();
            Container.Bind<GameUIView>().FromInstance(_gameUIView).AsSingle();
        }
    }
}
