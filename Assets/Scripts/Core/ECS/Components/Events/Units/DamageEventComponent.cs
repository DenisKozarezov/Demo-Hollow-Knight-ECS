using UnityEngine;

namespace Core.ECS.Events
{
    internal struct DamageEventComponent
    {
        public byte Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
