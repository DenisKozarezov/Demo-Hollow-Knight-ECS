using Core.ECS.Components.Units;
using Core.Services;
using Core.Models;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events.Player
{
    [Event(Self)] public sealed class PlayerDiedEvent : IComponent { }
    [Event(Self)] public sealed class PlayerObtainedGeoEvent : IComponent { public int Value; }
    [Event(Self)] public sealed class PlayerRecievedDamageEvent : IComponent { public int Value;  }
    [Event(Self)] public sealed class PlayerHealedEvent : IComponent { public int Value; }
    [Event(Self)] public sealed class EnergyReducedEvent : IComponent { public float Value; }
    [Event(Self)] public sealed class PlayerEnteredBossZoneEvent : IComponent { public EnemyModel BossModel; }
    [Event(Self)] public sealed class PlayerTalkingWithNPCEvent : IComponent { public NPCComponent NPC; }

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
