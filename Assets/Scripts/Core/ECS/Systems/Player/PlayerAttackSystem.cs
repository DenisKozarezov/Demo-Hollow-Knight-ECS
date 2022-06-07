using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<ColliderComponent, HealthComponent, HittableComponent>
            .Exclude<DiedComponent> _filter = null;

        private readonly PlayerInputController _playerInput;
        private float _damage = 5;
        private float _sqrDistance = 9f;
        
        private GameObject _gameObject;

        internal PlayerAttackSystem(PlayerInputController playerInputController) { _playerInput = playerInputController; }

        public virtual void Init()
        {
            // Input
            _playerInput.Keyboard.Attack.performed += OnAttack;

            // Player
            foreach (var i in _filter)
            {
                ref var collider = ref _filter.Get1(i);

                if (collider.Value.gameObject.layer == Constants.PlayerLayer)
                {
                    _gameObject = collider.Value.gameObject;
                    break;
                }
            }
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Attack.performed -= OnAttack;
        }

        private bool ReachedTarget(GameObject target)
        {
            if (target == null) return false;
            return (_gameObject.transform.position - target.transform.position).sqrMagnitude <= _sqrDistance;
        }
        private IEnumerable<EcsEntity> GetNearestEnemies()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var collider = _filter.Get1(i).Value;

                if (collider.gameObject == _gameObject) continue;

                // If player hits the enemy, return the target
                if (ReachedTarget(collider.gameObject))
                {
                    yield return entity;
                }
            }
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            foreach (var entity in GetNearestEnemies())
            {
                ref var collider = ref entity.Get<ColliderComponent>();           

                // Hit the enemy
                ref var damageComponent = ref entity.Get<DamageEventComponent>();
                damageComponent.Damage = _damage;
                damageComponent.Target = collider.Value.gameObject;
                damageComponent.Source = _gameObject;
            }
        }
    }
}