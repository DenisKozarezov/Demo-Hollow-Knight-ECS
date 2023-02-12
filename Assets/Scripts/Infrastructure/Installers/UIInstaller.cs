using UnityEngine;
using Zenject;
using Core.UI;

namespace Core.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private GameUIView _gameUIView;

        public override void InstallBindings()
        {
            Container.Bind<GameUIView>().FromInstance(_gameUIView).AsSingle();
        }
    }
}
