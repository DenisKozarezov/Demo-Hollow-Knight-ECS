using System.Collections.Generic;
using Entitas;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerStoppedMovingSystem : ReactiveSystem<GameEntity>
    {
        public PlayerStoppedMovingSystem(GameContext game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Moving.AddedOrRemoved());
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasMovable;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.isStoppedMoving = !entity.isMoving;
            }
        }
    }
}