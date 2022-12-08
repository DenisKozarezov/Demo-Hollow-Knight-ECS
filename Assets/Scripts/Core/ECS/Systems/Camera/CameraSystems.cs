namespace Core.ECS.Systems.Camera
{
    public class CameraSystems : Feature
    {
        public CameraSystems(GameContext context, UnityEngine.Camera camera) : base(context)
        {
            Add(new CameraShakeSystem(camera));
            Add(new CameraFadeSystem(camera));
            Add(new CameraLowHealthVignetteSystem(camera));
        }
    }
}
