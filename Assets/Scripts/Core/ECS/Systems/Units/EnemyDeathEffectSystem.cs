using UnityEngine;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems
{
    internal sealed class EnemyDeathEffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ColliderComponent, EnemyComponent, DiedComponent> _filter = null;

        public GameObject SpawnPrefab(Vector2 position)
        {
            return null;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                // Death effect
                var go = SpawnPrefab(collider.Value.transform.position);
            }
        }
    }
}