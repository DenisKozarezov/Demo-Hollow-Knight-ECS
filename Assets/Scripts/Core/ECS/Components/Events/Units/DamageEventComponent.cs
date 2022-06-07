using System;
using UnityEngine;

namespace Examples.Example_1.ECS.Events
{
    [Serializable]
    internal struct DamageEventComponent
    {
        public float Damage;
        public GameObject Target;
        public GameObject Source;
    }
}
