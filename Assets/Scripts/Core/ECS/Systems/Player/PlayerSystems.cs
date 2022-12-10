using Core.ECS.Events.Player;
using Core.Models;

namespace Core.ECS.Systems.Player
{
    public class PlayerSystems : Feature
    {
        public PlayerSystems(GameContext context) : base(context)
        {
            OneFrame<PlayerRecievedDamageEvent>();
            OneFrame<PlayerDiedEvent>();
            OneFrame<PlayerHealedEvent>();
            OneFrame<EnergyReducedEvent>();

            Add(new PlayerInitSystem(context.UnitsDefinitions.PlayerModel));
            Add(new PlayerRecievedDamageSystem());
            Add(new PlayerFocusSystem(context.InputSystem, context.UnitsDefinitions.PlayerModel.GetAbility<HealingFocusAbility>()));
            Add(new EnergySystem());
            Add(new PlayerDiedSystem());
            Add(new PlayerHealingSystem());
            Add(new PlayerMoveSystem(context.InputSystem));
            Add(new PlayerJumpSystem(context.InputSystem));
            Add(new PlayerAttackSystem(context.InputSystem));
            Add(new PlayerAttackCooldownSystem(context.InputSystem));
            Add(new PlayerAnimationSystem(context.InputSystem));
            Add(new PlayerInteractSystem(context.InputSystem));
            Add(new PlayerEnteredBossZoneSystem());
            Add(new PlayerObtainedGeoSystem());
        }
    }
}
