namespace Core.ECS.Systems.Camera
{
    internal class CameraSystems : Feature
    {
        internal CameraSystems(GameContext context) : base(context)
        {
            Add(new CameraShakeSystem(UnityEngine.Camera.main));
            Add(new CameraFadeSystem(UnityEngine.Camera.main));
            //Add(new CameraFollowSystem(UnityEngine.Camera.main));
        }
    }
}
