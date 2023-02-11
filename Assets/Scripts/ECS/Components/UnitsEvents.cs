using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using static Entitas.CodeGeneration.Attributes.EventTarget;

namespace Core.ECS.Events
{
    [Event(Self)] public sealed class HitEventComponent : IComponent
    {
        public int Damage;
        public Vector2 HitPosition;
        public float HitRadius;
        public int TargetLayer;
        public GameObject Source;
    }
    [Event(Self)] public sealed class AnimateDamageEventComponent : IComponent
    {
        public GameObject GameObjectRef;
        public float Duration;
        public bool Damaged;
    }
    [Event(Self)] public sealed class CreateDustEventComponent : IComponent
    {
        public Vector2 Point;
        public Vector3 Scale;
    }
}
