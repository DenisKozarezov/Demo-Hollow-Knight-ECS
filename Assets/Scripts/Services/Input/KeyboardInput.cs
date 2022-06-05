using System;
using UnityEngine;

namespace Core.Input
{
    public class KeyboardInput : IInputSystem
    {
        public Vector3 Direction => throw new NotImplementedException();

        public event Action Fire;
    }
}