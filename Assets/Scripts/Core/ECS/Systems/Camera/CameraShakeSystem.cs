using UnityEngine;
using System.Collections;
using Leopotam.Ecs;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    internal sealed class CameraShakeSystem: IEcsRunSystem
    {
        private readonly EcsFilter<AnimateCameraShakeEventComponent> _filter = null;

        private readonly MonoBehaviour _monoBehaviour;
        private const float ShakeForce = 0.2f;
        private bool _shaking;

        internal CameraShakeSystem(UnityEngine.Camera camera)
        {
            _monoBehaviour = camera.GetComponent<MonoBehaviour>();
        }

        public void Run()
        {
            if (_shaking) return;

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var component = ref _filter.Get1(i);
                _monoBehaviour.StartCoroutine(ShakeCoroutine(component.ShakeDuration, ShakeForce));
                entity.Destroy();
            }
        }

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
        {
            _shaking = true;
            float elapsedTime = 0f;
            Vector3 startPosition = _monoBehaviour.transform.localPosition;
            while (elapsedTime < duration)
            {
                _monoBehaviour.transform.localPosition = startPosition + Random.insideUnitSphere * magnitude;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _shaking = false;
            _monoBehaviour.transform.localPosition = startPosition;
        }
    }
}
