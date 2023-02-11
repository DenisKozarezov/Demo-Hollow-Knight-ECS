using UnityEngine;
using System.Collections;
using Core.ECS.Events;

namespace Core.ECS.Systems.Camera
{
    public sealed class CameraShakeSystem
    {
        //private readonly EcsFilter<CameraShakeEventComponent> _filter = null;
        //private readonly ICoroutineRunner _coroutineRunner;
        //private readonly Transform _transform;
        //private bool _shaking;

        //public CameraShakeSystem(ICoroutineRunner coroutineRunner, UnityEngine.Camera camera)
        //{
        //    _coroutineRunner = coroutineRunner;
        //    _transform = camera.transform;
        //}

        //void IEcsRunSystem.Run()
        //{
        //    if (_shaking) return;

        //    foreach (var i in _filter)
        //    {
        //        ref var entity = ref _filter.GetEntity(i);
        //        ref var @event = ref _filter.Get1(i);
        //        _coroutineRunner.StartCoroutine(ShakeCoroutine(@event.ShakeDuration, @event.ShakeForce));
        //        entity.Destroy();
        //    }
        //}

        //private IEnumerator ShakeCoroutine(float duration, float magnitude)
        //{
        //    _shaking = true;
        //    float elapsedTime = 0f;
        //    Vector3 startPosition = _transform.localPosition;
        //    while (elapsedTime < duration)
        //    {
        //        _transform.localPosition = startPosition + Random.insideUnitSphere * magnitude;
        //        elapsedTime += Time.deltaTime;
        //        yield return null;
        //    }
        //    _shaking = false;
        //    _transform.localPosition = startPosition;
        //}
    }
}
