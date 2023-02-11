using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems.UI
{
    public sealed class HealthViewInitSystem : ReactiveSystem<GameEntity>
    {
        private IGroup<GameEntity> _healthUI;

        public HealthViewInitSystem(GameContext game) : base(game)
        {
            _healthUI = game.GetGroup(GameMatcher.HealthUI);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Player.Added());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity player in entities)
            {
                foreach (GameEntity viewEntity in _healthUI)
                {
                    int maxHealth = player.maxHp.Value;
                    var view = viewEntity.healthUI.Value;
                    view.Init(maxHealth);
                    view.RegisterListeners(player);
                }
            }
        }       
    }
}
