using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Events.Player;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Player
{
    internal class PlayerRecievedDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DamageEventComponent, PlayerTagComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);

                // Raise event
                var damageEvent = _world.NewEntity().Get<PlayerRecievedDamageEvent>();
                var healthEvent = _world.NewEntity().Get<PlayerHealthModifiedEvent>().Delta = (short)(-component.Damage);
            }
        }
    }
}
