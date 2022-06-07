using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Examples.Example_1.ECS.Events;

namespace Examples.Example_1.ECS.Systems
{
    internal class GroundSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<ColliderComponent>.Exclude<OnGroundComponent> _filter = null;

        private bool OnGround(Collider2D collider, out Vector2 point)
        {
            ContactFilter2D contactFilter = new ContactFilter2D { layerMask = Constants.GroundLayer };
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            
            // Check collision
            int count = collider.GetContacts(contactFilter, contacts);

            // If not on ground
            if (count == 0)
            {
                point = Vector3.zero;
                return false;
            }

            // Return contact point
            float dx = collider.bounds.size.x * 0.5f;
            point = contacts[0].point + Vector2.right * dx;
            return true;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var collider = ref _filter.Get1(i);
                
                // If on ground then add component
                if (OnGround(collider.Value, out Vector2 point))
                {
                    entity.Get<OnGroundComponent>().Point = point;

                    // Create dust event
                    EcsEntity dustEntity = _world.NewEntity();
                    ref var dustComponent = ref dustEntity.Get<AnimateDustEventComponent>();
                    dustComponent.Point = point;
                    dustComponent.Scale = Vector3.one * 0.5f;
                }
            }
        }
    }
}
