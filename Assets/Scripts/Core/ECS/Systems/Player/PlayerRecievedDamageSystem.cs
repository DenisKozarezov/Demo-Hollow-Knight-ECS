using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Events.Player;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public class PlayerRecievedDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<
            HealthComponent, 
            DamageEventComponent, 
            ColliderComponent, 
            PlayerTagComponent> _filter = null;

        private const string HitEffectPath = "Prefabs/Effects/Impact/Hit Crack Impact";

        private GameObject CreateEffect(Vector2 position)
        {
            var asset = Resources.Load<GameObject>(HitEffectPath);
            return GameObject.Instantiate(asset, position, Quaternion.identity);
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);
                ref var damage = ref _filter.Get2(i);
                ref var collider = ref _filter.Get3(i);

                // Player recieved damage
                _world.NewEntity(new PlayerRecievedDamageEvent { Value = damage.Damage });

                // Create hit effect
                GameObject.Destroy(CreateEffect(collider.Value.bounds.center), 0.5f);

                // Player died
                if (health.Health == 0) _world.NewEntity<PlayerDiedEvent>();
            }
        }
    }
}
