using Core.Input;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputSystem>().To<KeyboardInput>().AsSingle().NonLazy();
    }
}