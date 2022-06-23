using System;
using UnityEngine;

namespace Core.Input
{
    internal interface IInputSystem
    {
        ref Vector2 Direction { get; }
        event Action Move;
        event Action Jump;
        event Action Attack;
        event Action FocusStarted;
        event Action FocusCanceled;
        event Action Pause;
        bool Enabled { get; }
        bool IsMoving { get; }

        void Enable();
        void Disable();
    }
}