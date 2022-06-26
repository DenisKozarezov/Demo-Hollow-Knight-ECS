using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal class GroundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent> _filter = null;

        private bool OnGround(Collider2D collider, out Vector2 point)
        {
            ContactFilter2D contactFilter = new ContactFilter2D { layerMask = 1 << Constants.GroundLayer };
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

        }
    }
}
