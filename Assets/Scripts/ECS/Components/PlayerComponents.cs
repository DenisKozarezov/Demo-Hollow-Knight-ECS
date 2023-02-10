using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using Core.Models;
using Core.ECS.Components.Units;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Components.Player
{
    public sealed class Player : IComponent { }
    public sealed class AttackCooldownComponent : IComponent { public float Value; }
    public sealed class CurrentEnergy : IComponent { public float Value; }
    public sealed class MaxEnergy : IComponent { public float Value; }
    public sealed class CurrentGeo : IComponent { public int Value; }
    public sealed class CanInteract : IComponent { }

    [Event(Self)] public sealed class ObtainedGeo : IComponent { public int Value; }
    [Event(Self)] public sealed class RestoredHealth : IComponent { public int Value; }
    [Event(Self)] public sealed class EnergyReduced : IComponent { public float Value; }
    [Event(Self)] public sealed class EnteredBossZone : IComponent { public EnemyModel BossModel; }
    [Event(Self)] public sealed class PlayerTalkingWithNPCEvent : IComponent { public NPC NPC; }

    // Input keys
    [Unique, Input] public sealed class LeftMouse : IComponent { }
    [Unique, Input] public sealed class RightMouse : IComponent { }
    [Unique, Input] public sealed class Keyboard : IComponent { }
    [Input] public sealed class MouseDown : IComponent { }
    [Input] public sealed class MouseWorldPosition : IComponent { public Vector2 Value; }
    [Input] public sealed class MouseScreenPosition : IComponent { public Vector2 Value; }
    [Input] public sealed class MouseUp : IComponent { }
    [Input] public sealed class Mouse : IComponent { }
    [Input] public sealed class Jump : IComponent { }
    [Input] public sealed class Horizontal : IComponent { public float Value; }
}