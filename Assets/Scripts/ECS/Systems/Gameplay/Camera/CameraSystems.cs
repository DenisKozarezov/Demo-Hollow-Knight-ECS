namespace Core.ECS.Systems.Camera
{
    public sealed class CameraSystems : Feature
    {
        public CameraSystems(Contexts contexts) : base(nameof(CameraSystems))
        {
            //Add(new CameraShakeSystem(context.CoroutineRunner, camera));
            //Add(new CameraFadeSystem(context.DiContainer.Resolve<Fader>()));
            Add(new CameraLowHealthVignetteSystem(contexts.game));
        }
    }
}
