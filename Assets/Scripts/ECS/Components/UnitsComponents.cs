using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Core.Models;
using Core.ECS.ViewListeners;
using static Entitas.CodeGeneration.Attributes.EventTarget;
using static Entitas.CodeGeneration.Attributes.CleanupMode;

namespace Core.ECS.Components.Units
{  
    // Unity Components
    public sealed class RigidbodyComponent : IComponent { public Rigidbody2D Value; }
    public sealed class ColliderComponent : IComponent { public Collider2D Value; }
    public sealed class AnimatorComponent : IComponent { public Animator Value; }
    public sealed class TransformComponent : IComponent { public Transform Value; }
    public sealed class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
    public sealed class BehaviourTreeComponent : IComponent { public BehaviourTree.Runtime.BehaviourTree BehaviourTree; }

    // Tags
    public sealed class Unit : IComponent { }
    public sealed class Enemy : IComponent { }
    public sealed class NPC : IComponent { public List<ConversationContext> Conversations; }

    // Characteristics
    public sealed class CurrentHp : IComponent { public int Value; }
    public sealed class MaxHp : IComponent { public int Value; }
    public sealed class Damage : IComponent { public int Value; }
    public sealed class AttackRange : IComponent { public float Value; }
    public sealed class Movable : IComponent { public float Value; }
    public sealed class JumpComponent : IComponent { public Vector2 JumpForceRange; }
    public sealed class CanAttack : IComponent { }
    public sealed class Dead : IComponent { }
    public sealed class Invulnerable : IComponent { }   
    public sealed class Channelling : IComponent { }

    public sealed class Hittable : IComponent { }
    public sealed class Collided : IComponent { }

    public sealed class ViewControllerComponent : IComponent { public IViewController Value; }

    [Event(Self)] public sealed class Attacking : IComponent { }
    [Event(Self)] public sealed class Jumping : IComponent { }
    [Event(Self)] public sealed class Grounded : IComponent { }
    [Event(Self)] public sealed class Moving : IComponent { }
    [Event(Self)] public sealed class Destroyed : IComponent { }
    [Event(Self), Cleanup(RemoveComponent)] public sealed class DamageTaken : IComponent { public int Value; }
    [Event(Self), Cleanup(RemoveComponent)] public sealed class StoppedMoving : IComponent { }
    [Event(Self), Cleanup(RemoveComponent)] public sealed class Died : IComponent { }

    [Game, Event(Self)] public sealed class Position : IComponent { public Vector2 Value; }
    [Game, Event(Self)] public sealed class Direction : IComponent { public float Value; }
}