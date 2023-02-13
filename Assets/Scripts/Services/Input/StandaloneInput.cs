using System;
using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class StandaloneInput : IInitializable, ITickable, ILateDisposable, IInputService
    {
        public Vector2 _direction;
        public float _jumpHoldTime;
        private PlayerInputController _playerInput;
        public ref Vector2 Direction => ref _direction;
        public ref float JumpHoldTime => ref _jumpHoldTime;
        public event Action FocusStarted;
        public event Action FocusCanceled;
        public bool IsLook => _playerInput.Keyboard.Look.IsPressed();
        public bool IsAttack => _playerInput.Keyboard.Attack.IsPressed();
        public bool IsJump { get; private set; }
        public bool IsPause => _playerInput.Keyboard.Pause.IsPressed();
        public bool Enabled => _playerInput.Keyboard.enabled;
        public bool IsMoving => Direction.x != 0;

        void IInitializable.Initialize()
        {
            _playerInput = new PlayerInputController();
            Enable();
        }
        void ITickable.Tick()
        {
            if (!Enabled) return;

            _direction.x = _playerInput.Keyboard.Move.ReadValue<float>();
            _direction.y = _playerInput.Keyboard.Look.ReadValue<float>();

            if (_playerInput.Keyboard.Jump.IsPressed()) JumpHoldTime += Time.deltaTime;
            else JumpHoldTime = 0f;

        }
        void ILateDisposable.LateDispose()
        {
            Disable();
        }
   
        public void Disable()
        {
            _playerInput.Disable();
        }
        public void Enable()
        {
            _playerInput.Enable();
        }
    }
}