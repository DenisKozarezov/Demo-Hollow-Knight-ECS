using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerCanInteractSystem : ReactiveSystem<GameEntity>
    {
        public PlayerCanInteractSystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Collided.AddedOrRemoved());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer && !entity.isDead;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.isCanInteract = entity.hasCollided;
            }
        }
    }
}
