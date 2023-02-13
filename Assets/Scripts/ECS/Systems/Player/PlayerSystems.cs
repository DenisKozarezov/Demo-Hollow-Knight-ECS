﻿namespace Core.ECS.Systems.Player
{
    public class PlayerSystems : Feature
    {
        public PlayerSystems(Contexts contexts) : base(nameof(PlayerSystems))
        {
            //Add(new PlayerRecievedDamageSystem());
            //Add(new PlayerFocusSystem(context.InputSystem, model.GetAbility<HealingFocusAbility>()));
            Add(new EnergySystem(contexts.game));
            //Add(new PlayerRespawnSystem());
            Add(new PlayerMoveSystem(contexts.game, contexts.input));
            Add(new PlayerStoppedMovingSystem(contexts.game));
            Add(new PlayerJumpSystem(contexts.game, contexts.input));
            Add(new PlayerHealingSystem(contexts.game));
            Add(new PlayerAttackSystem(contexts.game, contexts.input));
            //Add(new PlayerAttackCooldownSystem(context.InputSystem));
            Add(new PlayerCanInteractSystem(contexts.game));
            Add(new PlayerInteractingSystem(contexts.game, contexts.input));
            Add(new PlayerObtainedGeoSystem(contexts.game));
        }
    }
}
