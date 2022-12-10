using Leopotam.Ecs;
using Core.ECS.Components.UI;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems.Player
{
    public class PlayerEnteredBossZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerEnteredBossZoneEvent> _event = null;
        private readonly EcsFilter<GameViewComponent> _gameView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _event)
            {
                foreach (var i in _gameView)
                {
                    ref var entity = ref _event.GetEntity(@event);
                    ref var gameView = ref _gameView.Get1(i);
                    string displayName = _event.Get1(@event).BossModel.DisplayName;
                    gameView.View.AnnounceBoss(displayName);
                    entity.Destroy();
                }
            }
        }
    }
}
