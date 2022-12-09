using UnityEngine;

namespace Core.ECS.Events
{
    public struct DamageEventComponent
    {
        public int Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
