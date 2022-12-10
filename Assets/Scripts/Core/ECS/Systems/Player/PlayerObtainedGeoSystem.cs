using Leopotam.Ecs;
using Core.ECS.Components.Player;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems.Player
{
    public class PlayerObtainedGeoSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _event = null;
        private readonly EcsFilter<GeoComponent> _player = null;

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _event)
            {
                foreach (var pl in _player)
                {
                    ref var obtainedGeo = ref _event.Get1(@event);
                    ref var currentGeo = ref _player.Get1(pl);
                    currentGeo.Value += obtainedGeo.Value;
                }
            }
        }
    }
}
