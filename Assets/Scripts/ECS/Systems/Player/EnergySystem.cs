using System;
using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class EnergySystem : ReactiveSystem<GameEntity>
    {
        public EnergySystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.EnergyReduced, GameMatcher.Player, GameMatcher.CurrentEnergy, GameMatcher.MaxEnergy)
                .NoneOf(GameMatcher.Dead));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.currentEnergy.Value > 0f;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                float reducedEnergy = entity.energyReduced.Value;

                if (reducedEnergy == 0f) continue;

                float newEnergy = Math.Max(entity.currentEnergy.Value - reducedEnergy, 0f);
                entity.ReplaceCurrentEnergy(newEnergy);
            }
        }
    }
}
