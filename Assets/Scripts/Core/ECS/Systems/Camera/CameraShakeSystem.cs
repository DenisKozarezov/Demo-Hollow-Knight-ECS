using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    internal sealed class CameraShakeSystem: IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<AnimateCameraShakeEventComponent> _filter = null;

        private readonly Animator CameraRefAnimator;
        private readonly float _timeAliveSeconds = 0.7f;

        internal CameraShakeSystem(UnityEngine.Camera camera)
        {
            CameraRefAnimator = camera.GetComponent<Animator>();
        }

        public void Init()
        {
            CameraRefAnimator.enabled = false;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity(i);
                ref var shakeComponent = ref ecsEntity.Get<AnimateCameraShakeEventComponent>();

                // Shake effect
                CameraRefAnimator.enabled = true;

                // Effect lifetime
                shakeComponent.TimeAlive += Time.deltaTime;
                if (shakeComponent.TimeAlive > _timeAliveSeconds)
                {
                    Reset(ref ecsEntity);
                }
            }
        }

        private void Reset(ref EcsEntity ecsEntity)
        {
            CameraRefAnimator.enabled = false;
            ecsEntity.Del<AnimateCameraShakeEventComponent>();
        }
    }
}
