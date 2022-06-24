namespace Core.ECS.Systems.Player
{
    internal class PlayerSystems : Feature
    {
        internal PlayerSystems(GameContext context) : base(context)
        {
            Add(new PlayerInitSystem(context.UnitsDefinitions.PlayerModel));
            Add(new PlayerRecievedDamageSystem());
            Add(new PlayerFocusSystem(context.InputSystem));
            Add(new EnergySystem());
            Add(new PlayerDiedSystem());
            Add(new PlayerHealingSystem());
            Add(new PlayerMoveSystem(context.InputSystem));
            Add(new PlayerJumpSystem(context.InputSystem));
            Add(new PlayerAttackSystem(context.InputSystem));
            Add(new PlayerAttackCooldownSystem(context.InputSystem));
            Add(new PlayerAnimationSystem(context.InputSystem));
        }
    }
}
