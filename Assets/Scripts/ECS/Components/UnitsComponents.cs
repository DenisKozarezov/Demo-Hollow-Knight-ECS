using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Models;
using Entitas;

namespace Core.ECS.Components.Units
{
    public sealed class EntityInitComponent : IComponent { public EntityReference EntityReference; }   
    public sealed class RigidbodyComponent : IComponent { public Rigidbody2D Value; }
    public sealed class ColliderComponent : IComponent { public Collider2D Value; }
    public sealed class AnimatorComponent : IComponent { public Animator Value; }
    public sealed class TransformComponent : IComponent { public Transform Value; }
    public sealed class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
    public sealed class NPCComponent : IComponent { public List<ConversationContext> Conversations; }
    public sealed class BehaviourTreeComponent : IComponent
    {
        public BehaviourTree.Runtime.BehaviourTree BehaviourTree;
        [NonSerialized] public bool Initialized;
    }
    public sealed class HealthComponent : IComponent { public int Health; public int MaxHealth; }  
    public sealed class DamageComponent : IComponent { public int Damage; public float AttackRange; }
    public sealed class MovableComponent : IComponent { public float Value; }
    public sealed class JumpComponent : IComponent { public Vector2 JumpForceRange; }
    public sealed class OnGroundComponent : IComponent { public Vector2 Point; }
    public sealed class EnemyComponent : IComponent { public EnemyModel EnemyModel; }
    public sealed class UnitComponent : IComponent { }
    public sealed class DiedComponent : IComponent { }
    public sealed class HittableComponent : IComponent { }
    public sealed class InvulnerableComponent : IComponent { }   
    public sealed class ChannellingComponent : IComponent { }
    public sealed class FalseKnightTagComponent : IComponent { }
}