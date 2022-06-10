using UnityEngine;
using UnityEngine.InputSystem;
using Leopotam.Ecs;
using Examples.Example_1.ECS.Components.Player;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerAnimationSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsSystem
    {
        private readonly EcsFilter<AnimatorComponent, RigidbodyComponent, PlayerTagComponent> _filter = null;
       
        private readonly PlayerInputController _playerInput;
        private Animator _animator;
        private Rigidbody2D _rigidbody;

        // ==== ANIMATIONS KEYS ===
        private const string FALL_KEY = "IsJumping";
        private const string JUMP_KEY = "StartJump";
        private const string MOVE_KEY = "Move";
        private const string ATTACK_KEY = "Attack";
        // ========================
        
        private bool IsMoving => _playerInput.Keyboard.Move.ReadValue<Vector2>().sqrMagnitude > 0f;
        private bool IsFalling => _rigidbody.velocity.y < 0f;

        internal PlayerAnimationSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Attack.performed += OnAttack;         
            _playerInput.Keyboard.Move.performed += OnMove;

            // Initialize references
            _animator = _filter.Get1(0).Value;
            _rigidbody = _filter.Get2(0).Value;
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Attack.performed -= OnAttack;
            _playerInput.Keyboard.Move.performed -= OnMove;
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            _animator.SetTrigger(ATTACK_KEY);
        }        
        private void OnMove(InputAction.CallbackContext context)
        {
            _animator.SetBool(MOVE_KEY, IsMoving && !IsFalling);
        }
        public void Run()
        {
            if (IsFalling)
            {
                if (!_animator.GetBool(FALL_KEY))
                {
                    _animator.SetTrigger(JUMP_KEY);
                    _animator.SetBool(FALL_KEY, true);
                }
            }
            else _animator.SetBool(FALL_KEY, false);
        }
    }
}