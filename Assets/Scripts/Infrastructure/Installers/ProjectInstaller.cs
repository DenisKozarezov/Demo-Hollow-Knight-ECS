using Zenject;
using Core.Services;
using Core.ECS.ViewListeners;

namespace Core.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {        
            Container.Bind<ICoroutineRunnerService>().To<UnityCoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<ILogService>().To<UnityDebugLogService>().AsSingle().NonLazy();
            Container.Bind<IRegisterService<IViewController>>().To<UnityCollisionRegistry>().AsSingle().NonLazy();
        }
    }
}