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
        bool IsLook { get; }
        bool IsJump { get; }
        bool IsAttack { get; }
        event Action FocusStarted;
        event Action FocusCanceled;
        bool IsPause { get; }
        void Enable();
        void Disable();
    }
}