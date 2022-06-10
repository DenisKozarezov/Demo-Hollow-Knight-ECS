using System;
using UnityEngine;

namespace Core.Input
{
    internal interface IInputSystem
    {
        Vector2 Direction { get; }
        event Action<Vector2> Move;
        event Action Jump;
        event Action Attack;

        void Enable();
        void Disable();
    }
}