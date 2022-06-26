using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;
using Core.ECS.Components;

namespace Core.ECS.Systems.Player
{
    internal class PlayerDiedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerDiedEvent> _filter = null;
        private readonly EcsFilter<ColliderComponent, PlayerTagComponent> _player = null;

        private const string DeathBlow = "Prefabs/Effects/Player Death/Low Health Hit";
        private const string DeathParticle = "Prefabs/Effects/Player Death/Death Effect";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var deathBlow = Resources.Load<GameObject>(DeathBlow);
            var deathParticle = Resources.Load<GameObject>(DeathParticle);

            var effect = GameObject.Instantiate(deathBlow, position, Quaternion.identity);
            GameObject.Destroy(effect, 0.5f);
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
                    collider.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

                    // Death Effect
                    CreateDeathEffect(collider.bounds.center);                    
                }
            }
        }
    }
}
