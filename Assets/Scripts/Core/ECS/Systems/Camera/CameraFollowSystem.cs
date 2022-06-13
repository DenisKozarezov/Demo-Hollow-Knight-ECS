using UnityEngine;
using Leopotam.Ecs;
using Examples.Example_1.ECS.Components.Player;
using Examples.Example_1.ECS;

namespace Examples.Example_1.ECS.Systems
{
    internal class CameraFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpriteRendererComponent, PlayerTagComponent> _filter = null;
        private readonly Camera _camera;
        private Vector2 _velocity;
        private float _smoothTime = 0.2f;

        internal CameraFollowSystem(Camera camera)
        {
            _camera = camera;
        }

        private Vector3 GetPosition(Vector2 targetPos)
        {
            Vector3 result = Vector2.SmoothDamp(_camera.transform.position, targetPos, ref _velocity, _smoothTime);
            result.z = _camera.transform.position.z;
            return result;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var spriteRenderer = ref _filter.Get1(i);

                _camera.transform.position = GetPosition(spriteRenderer.Value.transform.position);
            }
        }
    }
}
