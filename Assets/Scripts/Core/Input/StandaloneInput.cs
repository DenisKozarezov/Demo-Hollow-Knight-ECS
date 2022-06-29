using System;
using UnityEngine;

namespace Core.Input
{
    [DisallowMultipleComponent]
    public class StandaloneInput : MonoBehaviour, IInputSystem
    {
        private PlayerInputController _playerInput;
        private Vector2 _direction;
        private float _jumpHoldTime;
        private bool _enabled;

        public ref Vector2 Direction => ref _direction;
        public ref float JumpHoldTime => ref _jumpHoldTime;
        public event Action Look;
        public event Action Jump;
        public event Action Attack;
        public event Action FocusStarted;
        public event Action FocusCanceled;
        public event Action Pause;
        public bool Enabled => _enabled;
        public bool IsMoving => _direction.x != 0;

        private void Awake()
        {
            _playerInput = new PlayerInputController();
            _playerInput.Keyboard.Jump.started += _ => Jump?.Invoke();
            _playerInput.Keyboard.Attack.started += _ => Attack?.Invoke();
            _playerInput.Keyboard.Pause.performed += _ => Pause?.Invoke();
            _playerInput.Keyboard.Focus.started += _ => FocusStarted?.Invoke();
            _playerInput.Keyboard.Focus.canceled += _ => FocusCanceled?.Invoke();
            _playerInput.Keyboard.Look.started += _ => Look?.Invoke();
        }
        private void Start()
        {
            Enable();
        }
        private void OnDestroy()
        {
            Disable();
        }
        private void Update()
        {
            if (!_enabled) return;

            _direction.x = _playerInput.Keyboard.Move.ReadValue<float>();
            _direction.y = _playerInput.Keyboard.Look.ReadValue<float>();

            if (_playerInput.Keyboard.Jump.IsPressed()) _jumpHoldTime += Time.deltaTime;
            else _jumpHoldTime = 0f;

        }
        public void Disable()
        {
            _enabled = false;
            _playerInput.Disable();
        }
        public void Enable()
        {
            _enabled = true;
            _playerInput.Enable();
        }
    }
}