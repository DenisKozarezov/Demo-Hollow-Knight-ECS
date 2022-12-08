using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Events.Player;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public class PlayerDiedSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerDiedEvent> _filter = null;
        private readonly EcsFilter<ColliderComponent, PlayerTagComponent> _player = null;

        private const string DeathBlow = "Prefabs/Effects/Player Death/Low Health Hit";
        private const string DeathParticle = "Prefabs/Effects/Player Death/Death Effect";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var deathBlow = Resources.Load<GameObject>(DeathBlow);
            var deathParticle = Resources.Load<GameObject>(DeathParticle);

            var effect = GameObject.Instantiate(deathBlow, position, Quaternion.identity);
            GameObject.Destroy(effect, 0.7f);
            GameObject.Instantiate(deathParticle, position, Quaternion.identity);
            return effect;
        }
        public void Run()
        {
            foreach (var i in _filter)
            {
                foreach (var pl in _player)
                {
                    // Lock physics
                    Collider2D collider = _player.Get1(pl).Value;

                    // Death Effect
                    CreateDeathEffect(collider.bounds.center);

                    // Camer Shake
                    _world.NewEntity(new AnimateCameraShakeEventComponent { ShakeDuration = 5f });
                }
            }
        }
    }
}
