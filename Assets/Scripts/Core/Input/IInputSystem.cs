using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInputSystem
    {
        ref Vector2 Direction { get; }
        ref float JumpHoldTime { get; }
        bool Enabled { get; }
        bool IsMoving { get; }
        event Action Look;
        event Action Jump;
        event Action Attack;
        event Action FocusStarted;
        event Action FocusCanceled;
        event Action Pause;
        void Enable();
        void Disable();
    }
}