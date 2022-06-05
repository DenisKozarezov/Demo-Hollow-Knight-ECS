using System.Collections.Generic;
using System.Linq;
using Examples.Example_1.ECS.Components.Player;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Examples.Example_1.ECS.Systems.Player
{
    public class PlayerAttackSystem : IEcsInitSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<PlayerMoveComponent> _filter = null;
        
        private PlayerInputController _playerInput;
        private float _damage = 5;
        private float _sqrDistance = 9f;
        
        private GameObject _gameObject;

        public PlayerAttackSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }
       
        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Attack.performed += OnAttack;
            _playerInput.Enable();
            
            // Initialize references
            ref var ecsEntity = ref _filter.GetEntity (0);
            _gameObject = ecsEntity.Get<PlayerAnimationComponent>().GameObject;
        }      
 
        private void OnAttack(InputAction.CallbackContext context)
        {
             GameObject enemy =
                 GameObject.FindObjectsOfType<GameObject>().
                     Where(i => i.layer == Constants.EnemyLayer).FirstOrDefault();

             if (enemy != null)
             {
                 var sqrDistance = (_gameObject.transform.position - enemy.transform.position).sqrMagnitude;
                
                 if (sqrDistance <= _sqrDistance)
                 {
                     foreach (var i in _filter)
                     {
                         EcsEntity attackAnimationEntity = _world.NewEntity();
                         ref var ecsEntity = ref _filter.GetEntity(i);

                         attackAnimationEntity.Get<AnimateDamageEventComponent>().GameObjectRef = enemy;
                     }     
                 }
             }           
        }
    }
}