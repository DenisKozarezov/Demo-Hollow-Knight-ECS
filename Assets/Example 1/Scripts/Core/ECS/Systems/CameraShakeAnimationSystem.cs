using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.ECS.Systems
{
    public class CameraShakeAnimationSystem: IEcsRunSystem, IEcsInitSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<AnimateCameraShakeEventComponent> _filter = null;
        private readonly Camera _camera;
        private readonly Animator CameraRefAnimator;

        private float _timeAliveSeconds = 0.7f;
        
        public CameraShakeAnimationSystem(Camera camera)
        {
            _camera = camera;
            CameraRefAnimator = _camera.GetComponent<Animator>();
        }

        public void Init()
        {
            CameraRefAnimator.enabled = false;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity (i);    
                CameraRefAnimator.enabled = true;
                ecsEntity.Get<AnimateCameraShakeEventComponent>().TimeAlive += Time.deltaTime;
                if (ecsEntity.Get<AnimateCameraShakeEventComponent>().TimeAlive > _timeAliveSeconds)
                {
                    CameraRefAnimator.enabled = false;
                    ecsEntity.Del<AnimateCameraShakeEventComponent>();
                }
            }
        }
    }
}
