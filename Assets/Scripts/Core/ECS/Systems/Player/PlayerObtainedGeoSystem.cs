using Core.ECS.Components.Player;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.Player
{
    internal class PlayerObtainedGeoSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _filter = null;
        private readonly EcsFilter<PlayerTagComponent> _player = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                foreach (var pl in _player)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var component = ref _filter.Get1(i);
                    ref var player = ref _player.GetEntity(pl);
                    player.Get<GeoComponent>().Value += component.Value;
                }
            }
        }
    }
}
