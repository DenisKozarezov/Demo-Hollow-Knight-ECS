using Core.ECS.Events.Player;

namespace Core.ECS.Systems.Player
{
    internal class PlayerSystems : Feature
    {
        internal PlayerSystems(GameContext context) : base(context)
        {
            Add(new PlayerInitSystem(context.UnitsDefinitions.PlayerModel));
            Add(new PlayerRecievedDamageSystem());
            Add(new PlayerMoveSystem(context.InputSystem));
            Add(new PlayerJumpSystem(context.InputSystem));
            Add(new PlayerAttackSystem(context.InputSystem));
            Add(new PlayerAttackCooldownSystem(context.InputSystem));
            Add(new PlayerAnimationSystem(context.InputSystem));
        }
    }
}
