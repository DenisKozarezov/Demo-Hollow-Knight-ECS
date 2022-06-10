using System;
using UnityEngine;
using Leopotam.Ecs;
using Examples.Example_1.ECS.ComponentProviders;

namespace Examples.Example_1.ECS
{    
    [Serializable] internal struct HealthComponent
    {
        public float Health;
        public float MaxHealth;
    }    
    [Serializable] internal struct RigidbodyComponent { public Rigidbody2D Value; }
    [Serializable] internal struct ColliderComponent { public Collider2D Value; }
    [Serializable] internal struct AnimatorComponent { public Animator Value; }
    [Serializable] internal struct SpriteRendererComponent { public SpriteRenderer Value; }
    [Serializable] internal struct UnitInitComponent { public EntityReference Value; }
    internal struct UnitComponent : IEcsIgnoreInFilter { }
    internal struct EnemyComponent : IEcsIgnoreInFilter { }
    internal struct DiedComponent : IEcsIgnoreInFilter { }
    internal struct HittableComponent : IEcsIgnoreInFilter { }
    internal struct InvulnerableComponent : IEcsIgnoreInFilter { }
    internal struct MovableComponent : IEcsIgnoreInFilter { }
    internal struct OnGroundComponent { public Vector2 Point; }
}