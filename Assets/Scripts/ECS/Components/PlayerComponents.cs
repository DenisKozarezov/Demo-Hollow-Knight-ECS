using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using Core.Models;
using Core.ECS.Components.Units;
using static Entitas.CodeGeneration.Attributes.EventTarget;
using static Entitas.CodeGeneration.Attributes.CleanupMode;

namespace Core.ECS.Components.Player
{
    public sealed class Player : IComponent { }
    public sealed class AttackCooldown : IComponent { public float Value; }
    public sealed class CurrentEnergy : IComponent { public float Value; }
    public sealed class MaxEnergy : IComponent { public float Value; }
    public sealed class CurrentGeo : IComponent { public int Value; }
    public sealed class CanInteract : IComponent { }
    public sealed class Interacting : IComponent { }

    // Player Events
    [Event(Self), Cleanup(RemoveComponent)] public sealed class RestoredHealth : IComponent { public int Value; }
    [Event(Any), Cleanup(RemoveComponent)] public sealed class EnergyReduced : IComponent { public float Value; }
    [Event(Any), Cleanup(RemoveComponent)] public sealed class PlayerTalkingWithNPCEvent : IComponent { public NPC NPC; }
    [Event(Any), Cleanup(RemoveComponent)] public sealed class EnteredBossZone : IComponent { public EnemyModel BossModel; }

    // Input Keys
    [Unique, Input] public sealed class LeftMouse : IComponent { }
    [Unique, Input] public sealed class RightMouse : IComponent { }
    [Unique, Input] public sealed class Keyboard : IComponent { }
    [Input] public sealed class MouseDown : IComponent { }
    [Input] public sealed class MouseWorldPosition : IComponent { public Vector2 Value; }
    [Input] public sealed class MouseScreenPosition : IComponent { public Vector2 Value; }
    [Input] public sealed class MouseUp : IComponent { }
    [Input] public sealed class Mouse : IComponent { }
    [Input] public sealed class Attack : IComponent { }
    [Input] public sealed class Jump : IComponent { }
    [Input] public sealed class Look : IComponent { }
    [Input] public sealed class Horizontal : IComponent { public float Value; }
    [Input] public sealed class Vertical : IComponent { public float Value; }
}