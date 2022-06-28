using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    internal class GeoObtainedUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _filter = null;
        private readonly EcsFilter<GeoViewComponent> _geoView = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                foreach (var geo in _geoView)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var view = ref _geoView.Get1(geo);
                    ref var value = ref _filter.Get1(i);

                    // Add geo
                    view.GeoView.AddValue(value.Value);

                    entity.Destroy();
                }
            }
        }
    }
}
