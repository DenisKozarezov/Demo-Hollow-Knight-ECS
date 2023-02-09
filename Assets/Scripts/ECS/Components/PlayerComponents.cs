using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using Leopotam.Ecs;
using Core.Models;
using Core.ECS.Components.Units;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Components.Player
{
    public sealed class PlayerComponent : IComponent { }
    public sealed class AttackCooldownComponent : IComponent { public float Value; }
    public sealed class EnergyComponent : IComponent { public float Energy; public float MaxEnergy; }
    public sealed class GeoComponent : IComponent { public int Value; }
    public sealed class CanInteractComponent : IComponent
    {
        public EcsEntity InteractableEntity;
        public InteractableComponent InteractableComponent;
    }

    [Event(Self)] public sealed class ObtainedGeo : IComponent { public int Value; }
    [Event(Self)] public sealed class RecievedDamage : IComponent { public int Value; }
    [Event(Self)] public sealed class RestoredHealth : IComponent { public int Value; }
    [Event(Self)] public sealed class EnergyReduced : IComponent { public float Value; }
    [Event(Self)] public sealed class EnteredBossZone : IComponent { public EnemyModel BossModel; }
    [Event(Self)] public sealed class PlayerTalkingWithNPCEvent : IComponent { public NPC NPC; }

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