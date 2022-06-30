using System;
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;
using Core.Models;

namespace Core.ECS.Components.Units
{
    [Serializable] internal struct UnitInitComponent { public EntityReference EntityReference; }   
    [Serializable] internal struct RigidbodyComponent { public Rigidbody2D Value; }
    [Serializable] internal struct ColliderComponent { public Collider2D Value; }
    [Serializable] internal struct AnimatorComponent { public Animator Value; }
    [Serializable] internal struct SpriteRendererComponent { public SpriteRenderer Value; }
    [Serializable] internal struct NPCComponent { public ConversationContext[] _conversations; }
    internal struct HealthComponent { public int Health; public int MaxHealth; }  
    internal struct DamageComponent { public int Damage; public float AttackRange; }
    internal struct MovableComponent { public float Value; }
    internal struct JumpComponent { public Vector2 JumpForceRange; }
    internal struct OnGroundComponent { public Vector2 Point; }
    internal struct UnitComponent : IEcsIgnoreInFilter { }
    internal struct EnemyComponent : IEcsIgnoreInFilter { }
    internal struct DiedComponent : IEcsIgnoreInFilter { }
    internal struct HittableComponent : IEcsIgnoreInFilter { }
    internal struct InvulnerableComponent : IEcsIgnoreInFilter { }   
    internal struct ChannellingComponent : IEcsIgnoreInFilter { }
    internal struct FalseKnightTagComponent : IEcsIgnoreInFilter { }
}