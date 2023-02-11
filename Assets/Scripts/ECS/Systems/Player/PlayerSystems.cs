namespace Core.ECS.Systems.Player
{
    public class PlayerSystems : Feature
    {
        public PlayerSystems(Contexts context) : base(nameof(PlayerSystems))
        {
            //Add(new PlayerInitSystem(model));
            //Add(new PlayerRecievedDamageSystem());
            //Add(new PlayerFocusSystem(context.InputSystem, model.GetAbility<HealingFocusAbility>()));
            //Add(new EnergySystem());
            Add(new PlayerDiedSystem(context.game));
            //Add(new PlayerRespawnSystem());
            Add(new PlayerMoveSystem(context.game, context.input));
            Add(new PlayerStoppedMovingSystem(context.game));
            Add(new PlayerJumpSystem(context.game, context.input));
            Add(new PlayerHealingSystem(context.game));
            //Add(new PlayerAttackSystem(context.InputSystem));
            //Add(new PlayerAttackCooldownSystem(context.InputSystem));
            //Add(new PlayerAnimationSystem(context.InputSystem));
            //Add(new PlayerCanInteractSystem());
            //Add(new PlayerInteractingSystem(context.InputSystem));
            //Add(new PlayerObtainedGeoSystem());
        }
    }
}
