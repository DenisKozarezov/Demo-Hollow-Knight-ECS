using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Components.Player
{
    internal struct PlayerTagComponent : IEcsIgnoreInFilter { }
    internal struct PlayerJumpComponent : IEcsIgnoreInFilter {}
    internal struct CanAttackComponent : IEcsIgnoreInFilter { }
}