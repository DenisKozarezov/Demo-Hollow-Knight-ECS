using System.Collections.Generic;
using Entitas;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerAttackSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _players;

        public PlayerAttackSystem(GameContext game, InputContext input) : base(input)
        {
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.CanAttack,
                    GameMatcher.Damage)
                .NoneOf(GameMatcher.Dead));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Attack.Added());
        }
        protected override bool Filter(InputEntity entity)
        {
            return entity.isAttack;
        }
        protected override void Execute(List<InputEntity> entities)
        {
            foreach (InputEntity input in entities)
            {
                foreach (GameEntity player in _players.GetEntities())
                {
                    AttackDirection direction = input.vertical.Value switch
                    {
                        0f  => AttackDirection.Default,
                        1f  => AttackDirection.Up,
                        -1f => AttackDirection.Down,
                        _   => AttackDirection.Default
                    };
                    player.AddAttacking(direction);
                }
            }
        }
    }
}