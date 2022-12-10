using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    [DisallowMultipleComponent]
    public class StandaloneInput : IInitializable, ITickable, ILateDisposable, IInputSystem
    {
        public Vector2 _direction;
        public float _jumpHoldTime;
        private PlayerInputController _playerInput;
        public ref Vector2 Direction => ref _direction;
        public ref float JumpHoldTime => ref _jumpHoldTime;
        public event Action Look;
        public event Action Jump;
        public event Action Attack;
        public event Action FocusStarted;
        public event Action FocusCanceled;
        public event Action Pause;
        public bool Enabled => _playerInput.Keyboard.enabled;
        public bool IsMoving => Direction.x != 0;

        void IInitializable.Initialize()
        {
            _playerInput = new PlayerInputController();
            _playerInput.Keyboard.Jump.started += _ => Jump?.Invoke();
            _playerInput.Keyboard.Attack.started += _ => Attack?.Invoke();
            _playerInput.Keyboard.Pause.performed += _ => Pause?.Invoke();
            _playerInput.Keyboard.Focus.started += _ => FocusStarted?.Invoke();
            _playerInput.Keyboard.Focus.canceled += _ => FocusCanceled?.Invoke();
            _playerInput.Keyboard.Look.started += _ => Look?.Invoke();
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