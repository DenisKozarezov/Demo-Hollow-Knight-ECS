using System;
using UnityEngine;

namespace Core.Services
{
    public interface IInputService
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