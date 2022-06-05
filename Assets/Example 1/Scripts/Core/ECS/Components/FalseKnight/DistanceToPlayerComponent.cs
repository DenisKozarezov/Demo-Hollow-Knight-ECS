using System;
using UnityEngine;

namespace Examples.Example_1.ECS.FalseKnight
{
    [Serializable]
    public struct DistanceToPlayerComponent
    {
        public GameObject PlayerRef;
        public GameObject GameObjectRef;
    }
}