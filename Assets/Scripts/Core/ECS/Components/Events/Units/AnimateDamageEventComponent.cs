using UnityEngine;

namespace Core.ECS.Events
{
    public struct AnimateDamageEventComponent
    {
        public GameObject GameObjectRef;
        public float Duration;
        public bool Damaged;
    }
}