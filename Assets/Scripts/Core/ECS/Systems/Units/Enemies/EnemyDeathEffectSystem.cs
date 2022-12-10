using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events;

namespace Core.ECS.Systems
{
    public sealed class EnemyDeathEffectSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<ColliderComponent, EnemyComponent, DiedComponent> _filter = null;

        private const string DeathParticle = "Prefabs/Effects/Player Death/Death Effect";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var asset = Resources.Load<GameObject>(DeathParticle);
            var effect = GameObject.Instantiate(asset, position, Quaternion.identity);
            return effect;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                Vector2 position = _filter.Get1(i).Value.bounds.center;

                // Death Effect
                CreateDeathEffect(position);

                // Camera Shake
                _world.NewEntity(new CameraShakeEventComponent { ShakeDuration = 5f, ShakeForce = 0.2f });
            }
        }
    }
}