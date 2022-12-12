using Leopotam.Ecs;

namespace Core.ECS.Components.Player
{
    public struct CanAttackComponent : IEcsIgnoreInFilter { }
    public struct PlayerTagComponent : IEcsIgnoreInFilter { }
    public struct AttackCooldownComponent { public float Value; }
    public struct EnergyComponent { public float Energy; public float MaxEnergy; }
    public struct GeoComponent { public int Value; }
    public struct CanInteractComponent 
    { 
        public EcsEntity InteractableEntity;
        public InteractableComponent InteractableComponent;
    }
}