using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    public class GeoObtainedUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _event = null;
        private readonly EcsFilter<GeoViewComponent> _geoView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _event)
            {
                foreach (var geo in _geoView)
                {
                    ref var entity = ref _event.GetEntity(@event);
                    ref var view = ref _geoView.Get1(geo);
                    ref int obtainedGeo = ref _event.Get1(@event).Value;
                    view.View.AddValue(obtainedGeo);
                    entity.Destroy();
                }
            }
        }
    }
}
