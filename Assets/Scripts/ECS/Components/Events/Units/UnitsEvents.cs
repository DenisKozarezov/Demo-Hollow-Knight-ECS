using UnityEngine;

namespace Core.ECS.Events
{
    public sealed class HitEventComponent
    {
        public int Damage;
        public Vector2 HitPosition;
        public float HitRadius;
        public int TargetLayer;
        public GameObject Source;
    }
    public sealed class DamageEventComponent
    {
        public int Damage;
        public GameObject Target;
        public GameObject Source;
    }
    public sealed class AnimateDamageEventComponent
    {
        public GameObject GameObjectRef;
        public float Duration;
        public bool Damaged;
    }
    public sealed class CreateUnitEventComponent
    {
        public uint ID;
        public Vector2 Point;
    }
    public sealed class CreateDustEventComponent
    {
        public Vector2 Point;
        public Vector3 Scale;
    }
}
