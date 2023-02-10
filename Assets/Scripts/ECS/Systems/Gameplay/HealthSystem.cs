using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class HealthSystem : ReactiveSystem<GameEntity>
    {
        public HealthSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.CurrentHp)
                .NoneOf(GameMatcher.Dead));
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCurrentHp;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.currentHp.Value <= 0f) entity.isDead = true;
            }
        } 
    }
}
