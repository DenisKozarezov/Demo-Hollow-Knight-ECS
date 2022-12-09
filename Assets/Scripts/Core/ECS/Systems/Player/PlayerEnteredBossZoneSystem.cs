using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.Player
{
    public class PlayerEnteredBossZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerEnteredBossZoneEvent> _filter = null;
        private readonly EcsFilter<GameViewComponent> _gameView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _gameView)
            {
                foreach (var @event in _filter)
                {
                    ref var entity = ref _filter.GetEntity(@event);
                    ref var gameView = ref _gameView.Get1(i);
                    string displayName = _filter.Get1(@event).BossModel.DisplayName;
                    gameView.View.AnnounceBoss(displayName);
                    entity.Destroy();
                }
            }
        }
    }
}
