using System;
using UnityEngine;

namespace Examples.Example_1.ECS.Components.Player
{
    [Serializable]
    public struct PlayerAnimationComponent
    {
        public Animator Animator;
        public GameObject GameObject;
        public GameObject Bottom;
    }
}