using UnityEngine;
using System.Collections;
using Leopotam.Ecs;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraShakeSystem: IEcsRunSystem
    {
        private readonly EcsFilter<CameraShakeEventComponent> _filter = null;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly UnityEngine.Camera _camera;
        private bool _shaking;

        public CameraShakeSystem(ICoroutineRunner coroutineRunner, UnityEngine.Camera camera)
        {
            _coroutineRunner = coroutineRunner;
            _camera = camera;
        }

        void IEcsRunSystem.Run()
        {
            if (_shaking) return;

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var component = ref _filter.Get1(i);
                _coroutineRunner.StartCoroutine(ShakeCoroutine(component.ShakeDuration, component.ShakeForce));
                entity.Destroy();
            }
        }

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
        {
            _shaking = true;
            float elapsedTime = 0f;
            Vector3 startPosition = _camera.transform.localPosition;
            while (elapsedTime < duration)
            {
                _camera.transform.localPosition = startPosition + Random.insideUnitSphere * magnitude;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _shaking = false;
            _camera.transform.localPosition = startPosition;
        }
    }
}
