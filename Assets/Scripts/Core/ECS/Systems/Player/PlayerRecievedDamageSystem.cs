using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Events.Player;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerRecievedDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<HealthComponent, DamageEventComponent, PlayerTagComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);
                ref var damage = ref _filter.Get2(i);

                // Player recieved damage
                _world.NewEntity().Get<PlayerRecievedDamageEvent>().Value = damage.Damage;
            
                // Player died
                if (health.Health == 0) _world.NewEntity().Get<PlayerDiedEvent>();
            }
        }
    }
}
