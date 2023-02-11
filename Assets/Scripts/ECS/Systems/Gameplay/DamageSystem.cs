using System;
using Entitas;

namespace Core.ECS.Systems
{
    public sealed class DamageSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        public DamageSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.DamageTaken, GameMatcher.CurrentHp, GameMatcher.Hittable)
                .NoneOf(GameMatcher.Dead, GameMatcher.Invulnerable));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                int damage = entity.damageTaken.Value;

                if (damage == 0) continue;

                int newHealth = Math.Max(entity.currentHp.Value - damage, 0);
                entity.ReplaceCurrentHp(newHealth);
            }
        }
    }
}
