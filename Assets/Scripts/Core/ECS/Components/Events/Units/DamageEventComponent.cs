using UnityEngine;

namespace Core.ECS.Events
{
    internal struct DamageEventComponent
    {
        public float Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
