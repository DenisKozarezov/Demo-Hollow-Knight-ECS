using Entitas;
using Leopotam.Ecs;

namespace Core.ECS.Components.Player
{
    public sealed class CanAttackComponent : IComponent { }
    public sealed class PlayerComponent : IComponent { }
    public sealed class AttackCooldownComponent : IComponent { public float Value; }
    public sealed class EnergyComponent : IComponent { public float Energy; public float MaxEnergy; }
    public sealed class GeoComponent : IComponent { public int Value; }
    public sealed class CanInteractComponent : IComponent
    {
        public EcsEntity InteractableEntity;
        public InteractableComponent InteractableComponent;
    }
}