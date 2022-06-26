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

        private const string DeathEffect = "Prefabs/Effects/Low Health Hit/Low Health Hit";

        private GameObject CreateDeathEffect(Vector2 position)
        {
            var asset = Resources.Load<GameObject>(DeathEffect);
            var effect = GameObject.Instantiate(asset, position, Quaternion.identity);
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
                    GameObject.Destroy(CreateDeathEffect(collider.bounds.center), 0.5f);                    
                }
            }
        }
    }
}
