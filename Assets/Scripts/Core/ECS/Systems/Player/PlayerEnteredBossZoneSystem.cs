using Core.ECS.Components.UI;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems.Player
{
    public class PlayerEnteredBossZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerEnteredBossZoneEvent> _filter = null;
        private readonly EcsFilter<GameViewComponent> _gameView = null;

        public void Run()
        {
            foreach (var i in _gameView)
            {
                foreach (var @event in _filter)
                {
                    ref var entity = ref _filter.GetEntity(@event);
                    ref var gameView = ref _gameView.Get1(i);
                    ref var bossEvent = ref _filter.Get1(@event);
                    gameView.View.AnnounceBoss(bossEvent.BossModel.DisplayName);
                    entity.Destroy();
                }
            }
        }
    }
}
