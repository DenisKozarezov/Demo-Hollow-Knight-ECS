using System;
using UnityEngine;

namespace Examples.Example_1.ECS.FalseKnight
{
    [Serializable]
    public struct FalseKnightHealthComponent
    {
        [NonSerialized] public float Health;
        public GameObject GameObjectRef;
    }
}