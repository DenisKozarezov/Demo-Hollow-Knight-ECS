using System.Collections.Generic;
using Examples.Example_1.ECS.Components.Player;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    public class PlayerAnimationSystem : IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<PlayerAnimationComponent> _filter = null;
       
        private PlayerInputController _playerInput;
        private GameObject _gameObject;
        private Animator _animator;
        private Rigidbody2D _body;

        // ==== ANIMATIONS KEYS ===
        private const string FALL_KEY = "IsJumping";
        private const string JUMP_KEY = "StartJump";
        private const string MOVE_KEY = "Move";
        private const string ATTACK_KEY = "Attack";
        // ========================
        
        private bool IsFalling => _body.velocity.y < 0f;
        private bool IsMoving => _playerInput.Keyboard.Move.ReadValue<Vector2>().sqrMagnitude > 0f;

        public PlayerAnimationSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
        
        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Attack.performed += OnAttack;         
            _playerInput.Keyboard.Move.performed += OnMove;
            _playerInput.Enable();
            
            // Initialize references
            ref var ecsEntity = ref _filter.GetEntity(0);
            _gameObject = ecsEntity.Get<PlayerAnimationComponent>().GameObject;
            _animator = ecsEntity.Get<PlayerAnimationComponent>().Animator;
            _body = _gameObject.GetComponent<Rigidbody2D>();
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
                if (_animator.GetBool(FALL_KEY) == false) 
                {
                    _animator.SetTrigger(JUMP_KEY);
                    _animator.SetBool(FALL_KEY, true);
                }
            }
            else if (_animator.GetBool(FALL_KEY))
            {
                ContactFilter2D contactFilter2D = new ContactFilter2D();
                contactFilter2D.layerMask = Constants.GroundLayer;

                List<Collider2D> contacts = new List<Collider2D>();
                if (Physics2D.GetContacts(_gameObject.GetComponent<BoxCollider2D>(), contactFilter2D, contacts) > 0)
                {
                    _animator.SetBool(FALL_KEY, false);
                    
                    foreach (var i in _filter)
                    {
                        EcsEntity dustAnimationEntity = _world.NewEntity();
                        ref var ecsEntity = ref _filter.GetEntity(i);

                        dustAnimationEntity.Get<AnimateDustEventComponent>().Parent 
                            = ecsEntity.Get<PlayerAnimationComponent>().Bottom.transform;
                        dustAnimationEntity.Get<AnimateDustEventComponent>().Scale = Vector3.one * 0.5f;
                    }     
                }
                    
            }
        }
    }
}