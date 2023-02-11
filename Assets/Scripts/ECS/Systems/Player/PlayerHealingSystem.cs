using System;
using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class PlayerHealingSystem : ReactiveSystem<GameEntity>
    {
        public PlayerHealingSystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.RestoredHealth, GameMatcher.Player, GameMatcher.CurrentHp, GameMatcher.MaxHp)
                .NoneOf(GameMatcher.Dead));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.currentHp.Value > 0 && entity.currentHp.Value < entity.maxHp.Value;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                int restoredHealth = entity.restoredHealth.Value;

                if (restoredHealth == 0) continue;

                int newHealth = Math.Min(entity.currentHp.Value + restoredHealth, entity.maxHp.Value);
                entity.ReplaceCurrentHp(newHealth);
            }
        }
    }
}
