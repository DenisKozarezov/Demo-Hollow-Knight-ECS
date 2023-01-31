using Core.ECS.Events.Player;
using Core.Models;

namespace Core.ECS.Systems.Player
{
    public class PlayerSystems : Feature
    {
        public PlayerSystems(GameContext context) : base(context)
        {
            PlayerModel model = context.UnitsModelsProvider.Resolve<PlayerModel>();

            OneFrame<PlayerRecievedDamageEvent>();
            OneFrame<PlayerDiedEvent>();
            OneFrame<PlayerHealedEvent>();
            OneFrame<EnergyReducedEvent>();

            Add(new PlayerInitSystem(model));
            Add(new PlayerRecievedDamageSystem());
            Add(new PlayerFocusSystem(context.InputSystem, model.GetAbility<HealingFocusAbility>()));
            Add(new EnergySystem());
            Add(new PlayerDiedSystem());
            //Add(new PlayerRespawnSystem());
            Add(new PlayerHealingSystem());
            Add(new PlayerMoveSystem(context.InputSystem));
            Add(new PlayerJumpSystem(context.InputSystem));
            Add(new PlayerAttackSystem(context.InputSystem));
            Add(new PlayerAttackCooldownSystem(context.InputSystem));
            Add(new PlayerAnimationSystem(context.InputSystem));
            Add(new PlayerCanInteractSystem());
            Add(new PlayerInteractingSystem(context.InputSystem));
            Add(new PlayerObtainedGeoSystem());
        }
    }
}
