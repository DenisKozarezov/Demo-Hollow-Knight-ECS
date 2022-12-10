using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;
using Core.Models;

namespace Core.ECS.Components.Units
{
    [Serializable] public struct EntityInitComponent { public EntityReference EntityReference; }   
    [Serializable] public struct RigidbodyComponent { public Rigidbody2D Value; }
    [Serializable] public struct ColliderComponent { public Collider2D Value; }
    [Serializable] public struct AnimatorComponent { public Animator Value; }
    [Serializable] public struct SpriteRendererComponent { public SpriteRenderer Value; }
    [Serializable] public struct NPCComponent { public List<ConversationContext> Conversations; }
    public struct HealthComponent { public int Health; public int MaxHealth; }  
    public struct DamageComponent { public int Damage; public float AttackRange; }
    public struct MovableComponent { public float Value; }
    public struct JumpComponent { public Vector2 JumpForceRange; }
    public struct OnGroundComponent { public Vector2 Point; }
    public struct UnitComponent : IEcsIgnoreInFilter { }
    public struct EnemyComponent : IEcsIgnoreInFilter { }
    public struct DiedComponent : IEcsIgnoreInFilter { }
    public struct HittableComponent : IEcsIgnoreInFilter { }
    public struct InvulnerableComponent : IEcsIgnoreInFilter { }   
    public struct ChannellingComponent : IEcsIgnoreInFilter { }
    public struct FalseKnightTagComponent : IEcsIgnoreInFilter { }
}