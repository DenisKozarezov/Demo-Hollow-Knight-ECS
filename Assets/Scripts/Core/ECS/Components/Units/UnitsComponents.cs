using System;
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;

namespace Core.ECS.Components.Units
{    
    [Serializable] internal struct UnitInitComponent { public EntityReference EntityReference; }   
    [Serializable] internal struct RigidbodyComponent { public Rigidbody2D Value; }
    [Serializable] internal struct ColliderComponent { public Collider2D Value; }
    [Serializable] internal struct AnimatorComponent { public Animator Value; }
    [Serializable] internal struct SpriteRendererComponent { public SpriteRenderer Value; }
    internal struct HealthComponent { public byte Health; public byte MaxHealth; }  
    internal struct DamageComponent { public byte Damage; public float AttackRange; }
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