using System;
using UnityEngine;

namespace Examples.Example_1.ECS.FalseKnight
{
    [Serializable]
    public struct FalseKnightAnimationComponent
    {
        public Animator Animator;
        public GameObject GameObject;
        public GameObject Bottom;
    }
}