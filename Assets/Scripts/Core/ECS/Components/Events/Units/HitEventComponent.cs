using UnityEngine;

namespace Core.ECS.Events
{
    internal struct HitEventComponent
    {
        public int Damage;
        public Vector2 HitPosition;
        public float HitRadius;
        public int TargetLayer;
        public GameObject Source;
    }
}
