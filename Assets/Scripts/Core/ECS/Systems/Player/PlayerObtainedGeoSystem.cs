using Core.ECS.Components.Player;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.Player
{
    public class PlayerObtainedGeoSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _filter = null;
        private readonly EcsFilter<GeoComponent> _player = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                foreach (var pl in _player)
                {
                    ref var obtainedGeo = ref _filter.Get1(i);
                    ref var currentGeo = ref _player.Get1(pl);
                    currentGeo.Value += obtainedGeo.Value;
                }
            }
        }
    }
}
