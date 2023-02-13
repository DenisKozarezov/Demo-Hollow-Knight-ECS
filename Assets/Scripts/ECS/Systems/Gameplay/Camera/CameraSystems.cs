namespace Core.ECS.Systems.Camera
{
    public sealed class CameraSystems : Feature
    {
        public CameraSystems(Contexts contexts) : base(nameof(CameraSystems))
        {
            Add(new CameraLowHealthVignetteSystem(contexts.game));
            Add(new CameraShakeAfterDeathSystem(contexts.game));
        }
    }
}
