using Core.UI;
using Leopotam.Ecs;

namespace Core.ECS.Components.Player
{
    internal struct CanAttackComponent : IEcsIgnoreInFilter { }
    internal struct ClimbingComponent : IEcsIgnoreInFilter { }
    internal struct SwimmingComponent : IEcsIgnoreInFilter { }
    internal struct PlayerTagComponent : IEcsIgnoreInFilter { }
    internal struct CanInteractComponent { public InteractableView View; }
    internal struct AttackCooldownComponent { public float Value; }
    internal struct EnergyComponent { public float Energy; public float MaxEnergy; }
    internal struct GeoComponent { public int Value; }
}