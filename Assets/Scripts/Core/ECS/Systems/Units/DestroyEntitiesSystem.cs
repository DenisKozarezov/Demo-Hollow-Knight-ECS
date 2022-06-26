using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DestroyEntitiesSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent, DiedComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                Collider2D collider = _filter.Get1(i).Value;
                collider.attachedRigidbody.simulated = false;
                collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                collider.enabled = false;
                collider.gameObject.layer = Constants.IgnoreRaycastLayer;
                entity.Destroy();
            }
        }
    }
}