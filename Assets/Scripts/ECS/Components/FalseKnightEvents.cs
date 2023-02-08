using Entitas;
using Entitas.CodeGeneration.Attributes;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events.FalseKnight
{
    [Event(Self)] public sealed class FalseKnightStrongAttackEvent : IComponent { }
    [Event(Self)] public sealed class FalseKnightJumpEvent : IComponent { }
    [Event(Self)] public sealed class FalseKnightRollEvent : IComponent { }
    [Event(Self)] public sealed class FalseKnightAttackEventComponent : IComponent { }
}