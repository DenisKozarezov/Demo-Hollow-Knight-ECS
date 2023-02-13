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
                    switch (input.vertical.Value)
                    {
                        case 0f:    player.AddAttacking(AttackDirection.Default);   break;
                        case 1f:    player.AddAttacking(AttackDirection.Up);        break;
                        case -1f:   player.AddAttacking(AttackDirection.Down);      break;
                        default:    player.AddAttacking(AttackDirection.Default);   break;
                    }
                }
            }
        }
    }
}