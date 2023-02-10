using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Core.Models;
using Core.ECS.ViewListeners;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Components.Units
{  
    public sealed class RigidbodyComponent : IComponent { public Rigidbody2D Value; }
    public sealed class ColliderComponent : IComponent { public Collider2D Value; }
    public sealed class AnimatorComponent : IComponent { public Animator Value; }
    public sealed class TransformComponent : IComponent { public Transform Value; }
    public sealed class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
    public sealed class NPC : IComponent { public List<ConversationContext> Conversations; }
    public sealed class BehaviourTreeComponent : IComponent
    {
        public BehaviourTree.Runtime.BehaviourTree BehaviourTree;
        [NonSerialized] public bool Initialized;
    }

    public sealed class CurrentHp : IComponent { public float Value; }
    public sealed class MaxHp : IComponent { public float Value; }
    public sealed class Damage : IComponent { public float Value; }
    public sealed class AttackRange : IComponent { public float Value; }
    public sealed class Movable : IComponent { public float Value; }
    public sealed class JumpComponent : IComponent { public Vector2 JumpForceRange; }
    public sealed class EnemyComponent : IComponent { public EnemyModel EnemyModel; }
    public sealed class CanAttack : IComponent { }
    public sealed class UnitComponent : IComponent { }
    public sealed class Dead : IComponent { }
    public sealed class Invulnerable : IComponent { }   
    public sealed class Channelling : IComponent { }
    public sealed class FalseKnight : IComponent { }

    public sealed class Hittable : IComponent { }
    public sealed class Collided : IComponent { }

    public sealed class ViewControllerComponent : IComponent { public IViewController Value; }

    [Event(Self)] public sealed class Attacking : IComponent { }
    [Event(Self)] public sealed class DamageTaken : IComponent { }
    [Event(Self)] public sealed class Died : IComponent { }
    [Event(Self)] public sealed class Jumping : IComponent { }
    [Event(Self)] public sealed class Grounded : IComponent { }
    [Event(Self)] public sealed class StoppedMoving : IComponent { }
    [Event(Self)] public sealed class Moving : IComponent { }
    [Event(Self)] public sealed class Destroyed : IComponent { }

    [Game, Event(Self)] public sealed class Position : IComponent { public Vector2 Value; }
    [Game, Event(Self)] public sealed class Direction : IComponent { public float Value; }
}