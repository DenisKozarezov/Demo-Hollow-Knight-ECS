using UnityEngine.Rendering.Universal;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraSystems : Feature
    {
        public CameraSystems(GameContext context, UnityEngine.Camera camera) : base(context)
        {
            Add(new CameraShakeSystem(context.CoroutineRunner, camera));
            //Add(new CameraFadeSystem(camera));
            Add(new CameraLowHealthVignetteSystem(camera.GetPostProcessSetting<Vignette>()));

            OneFrame<CameraFadeEventComponent>();
        }
    }
}
