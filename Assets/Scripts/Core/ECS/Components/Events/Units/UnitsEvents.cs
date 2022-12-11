using UnityEngine;

namespace Core.ECS.Events
{
    public struct HitEventComponent
    {
        public int Damage;
        public Vector2 HitPosition;
        public float HitRadius;
        public int TargetLayer;
        public GameObject Source;
    }
    public struct DamageEventComponent
    {
        public int Damage;
        public GameObject Target;
        public GameObject Source;
    }
    public struct AnimateDamageEventComponent
    {
        public GameObject GameObjectRef;
        public float Duration;
        public bool Damaged;
    }
    public struct CreateUnitEventComponent
    {
        public uint ID;
        public Vector2 Point;
    }
    public struct CreateDustEventComponent
    {
        public Vector2 Point;
        public Vector3 Scale;
    }
}
