using System.Collections.Generic;
using Examples.Example_1.ECS.Components.Player;
using Examples.Example_1.ECS.Events;
using Examples.Example_1.ECS.FalseKnight;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.FalseKnight
{
    public class FalseKnightJumpAnimationSystem: IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<FalseKnightAnimationComponent> _filter = null;
        
        private GameObject _gameObject;
        private Animator _animator;

        private Transform _transform;
        private Rigidbody2D _body;
        
        
        public virtual void Init()
        {
            ref var ecsEntity = ref _filter.GetEntity (0);

            _gameObject = ecsEntity.Get<FalseKnightAnimationComponent>().GameObject;
            _animator = ecsEntity.Get<FalseKnightAnimationComponent>().Animator;
            _body = _gameObject.GetComponent<Rigidbody2D>();
            _transform = _gameObject.transform;
        }

        private void OnAttack()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity (i);
                ecsEntity.Get<FalseKnightAnimationComponent>().Animator.SetTrigger("Attack"); //   GetComponent<Transform>();
            }  
        }
        
        private void OnMove(InputAction.CallbackContext context)
        {
           
        }
        
        public void Run()
        {
            if (Mathf.Abs(_body.velocity.y) > 0.1f)
            {
                if (_animator.GetBool("IsJumping") == false) {
                    _animator.SetTrigger("Jump");
                    _animator.SetBool("IsJumping", true);
                }
            }
            else if (_animator.GetBool("IsJumping") == true)
            {
                ContactFilter2D contactFilter2D = new ContactFilter2D();
                contactFilter2D.layerMask = Constants.GroundLayer;

                List<Collider2D> contacts = new List<Collider2D>();
                if (Physics2D.GetContacts(_gameObject.GetComponent<BoxCollider2D>(), contactFilter2D, contacts) > 0)
                {
                    _animator.SetBool("IsJumping", false);
                    _animator.SetTrigger("Land");
                    
                    foreach (var i in _filter)
                    {
                        EcsEntity dustAnimationEntity = _world.NewEntity();
                        EcsEntity cameraShakeAnimationEntity = _world.NewEntity();
                        ref var ecsEntity = ref _filter.GetEntity (i);

                        dustAnimationEntity.Get<AnimateDustEventComponent>().Parent 
                            = ecsEntity.Get<FalseKnightAnimationComponent>().Bottom.transform;
                        dustAnimationEntity.Get<AnimateDustEventComponent>().Scale = new Vector3(1f, 1f, 1f);

                        cameraShakeAnimationEntity.Get<AnimateCameraShakeEventComponent>();
                    }     
                }
            }
        }
    }
}