using System.Collections.Generic;
using UnityEngine;
using Core.ECS.Events;
using Leopotam.Ecs;
using AI.ECS;

namespace Core.ECS.Systems
{
    public class HitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HitEventComponent> _filter = null;

        private void HitAllTargets(IEnumerable<Collider2D> targets, ref HitEventComponent component)
        {
            foreach (Collider2D target in targets)
            {
                if (target == null) continue;

                var entityRef = target.GetComponent<EntityReference>();
                if (entityRef.Entity.IsNullOrEmpty()) continue;
                
                ref var damageEntity = ref entityRef.Entity.Get<DamageEventComponent>();
                damageEntity.Damage = component.Damage;
                damageEntity.Target = target.gameObject;
                damageEntity.Source = component.Source;
            }
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                Collider2D[] targets = new Collider2D[5]; 
                int hits = Physics2D.OverlapCircleNonAlloc(component.HitPosition, component.HitRadius, targets, 1 << component.TargetLayer);
                
                // Missed
                if (hits == 0) continue;

                // Damage all targets
                HitAllTargets(targets, ref component);
            }
        }
    }
}
