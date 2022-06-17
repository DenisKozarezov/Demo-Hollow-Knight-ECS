using System;
using UnityEngine;

namespace Core.ECS.Events
{
    [Serializable]
    internal struct DamageEventComponent
    {
        public float Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
