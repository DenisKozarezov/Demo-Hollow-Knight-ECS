using Core.ECS.Events.Player;
using Core.UI;
using Leopotam.Ecs;

namespace Core.ECS.Systems.UI
{
    public class GeoObtainedUISystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObtainedGeoEvent> _event = null;
        private readonly GeoUIView _view;

        public GeoObtainedUISystem(GeoUIView view)
        {
            _view = view;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _event)
            {
                ref var entity = ref _event.GetEntity(i);
                ref int obtainedGeo = ref _event.Get1(i).Value;
                _view.AddValue(obtainedGeo);
                entity.Destroy();
            }
        }
    }
}
