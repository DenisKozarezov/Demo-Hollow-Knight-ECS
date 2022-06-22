namespace Core.ECS.Systems.Camera
{
    internal class CameraSystems : Feature
    {
        internal CameraSystems(GameContext context, UnityEngine.Camera camera) : base(context)
        {
            Add(new CameraShakeSystem(camera));
            Add(new CameraFadeSystem(camera));
            Add(new CameraLowHealthVignetteSystem(camera));
        }
    }
}
