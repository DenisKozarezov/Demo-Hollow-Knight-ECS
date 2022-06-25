using UnityEngine;

namespace Core.ECS.Events
{
    internal struct DamageEventComponent
    {
        public int Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
