using System;
using UnityEngine;

namespace Core.Input
{
    [DisallowMultipleComponent]
    public class StandaloneInput : MonoBehaviour, IInputSystem
    {
        private PlayerInputController _playerInput;
        private Vector2 _direction;
        private bool _enabled;

        public ref Vector2 Direction => ref _direction;
        public event Action Move;
        public event Action Jump;
        public event Action Attack;
        public event Action FocusStarted;
        public event Action FocusCanceled;
        public event Action Pause;
        public bool Enabled => _enabled;
        public bool IsMoving => _direction.sqrMagnitude > 0;

        private void Awake()
        {
            _playerInput = new PlayerInputController();
            _playerInput.Keyboard.Move.started += _ => Move?.Invoke();
            _playerInput.Keyboard.Jump.started += _ => Jump?.Invoke();
            _playerInput.Keyboard.Attack.started += _ => Attack?.Invoke();
            _playerInput.Keyboard.Pause.performed += _ => Pause?.Invoke();
            _playerInput.Keyboard.Focus.started += _ => FocusStarted?.Invoke();
            _playerInput.Keyboard.Focus.canceled += _ => FocusCanceled?.Invoke();
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

            _direction = _playerInput.Keyboard.Move.ReadValue<Vector2>();
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