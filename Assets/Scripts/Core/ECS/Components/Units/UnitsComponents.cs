using System;
using UnityEngine;
using Leopotam.Ecs;
using AI.ECS;
using Core.Models;

namespace Examples.Example_1.ECS
{    
    [Serializable] internal struct UnitInitComponent 
    {
        public UnitModel UnitModel;
        public EntityReference EntityReference; 
    }   
    [Serializable] internal struct RigidbodyComponent { public Rigidbody2D Value; }
    [Serializable] internal struct ColliderComponent { public Collider2D Value; }
    [Serializable] internal struct AnimatorComponent { public Animator Value; }
    [Serializable] internal struct SpriteRendererComponent { public SpriteRenderer Value; }
    internal struct HealthComponent { public float Health; public float MaxHealth; }  
    internal struct MovableComponent { public float Value; }
    internal struct OnGroundComponent { public Vector2 Point; }
    internal struct UnitComponent : IEcsIgnoreInFilter { }
    internal struct EnemyComponent : IEcsIgnoreInFilter { }
    internal struct DiedComponent : IEcsIgnoreInFilter { }
    internal struct HittableComponent : IEcsIgnoreInFilter { }
    internal struct InvulnerableComponent : IEcsIgnoreInFilter { }   
}