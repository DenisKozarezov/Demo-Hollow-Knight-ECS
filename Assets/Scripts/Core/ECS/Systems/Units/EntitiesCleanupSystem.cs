using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using DG.Tweening;

namespace Core.ECS.Systems
{
    public sealed class EntitiesCleanupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent, SpriteRendererComponent, DiedComponent> _filter = null;

        private void DisableCollision(Collider2D collider)
        {
            collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            collider.enabled = false;
            collider.gameObject.layer = Constants.IgnoreRaycastLayer;
        }
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                SpriteRenderer renderer = _filter.Get2(i).Value;
                renderer.DOColor(Color.black, 1f);

                DisableCollision(_filter.Get1(i).Value);
                
                entity.Destroy();
            }
        }
    }
}