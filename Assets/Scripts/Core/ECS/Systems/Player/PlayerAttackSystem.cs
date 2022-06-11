using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;
using Examples.Example_1.ECS.Components.Player;
using Core.Units;
using Examples.Example_1.ECS.ComponentProviders;
using AI.ECS;
using Core.Models;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<ColliderComponent, HealthComponent, HittableComponent>
            .Exclude<PlayerTagComponent, DiedComponent> _filter = null;

        private readonly PlayerInputController _playerInput;
        private readonly UnitScript _player = null;
        private readonly PlayerModel _playerModel;
        private const string ATTACK_KEY = "Attack";      
                     
        private EntityReference _entity;
        private Animator Animator;    

        private float SqrAttackRange => _playerModel.AttackRange * _playerModel.AttackRange;
        private bool CanAttack => _entity.Entity.Has<CanAttackComponent>();

        internal PlayerAttackSystem(PlayerInputController playerInputController, PlayerModel playerModel)
        {
            _playerInput = playerInputController;
            _playerModel = playerModel;
        }

        public void Init()
        {
            _playerInput.Keyboard.Attack.started += OnAttack;
            _entity = _player.GetComponent<EntityReference>();
            Animator = _player.GetComponent<Animator>();
        }
        public void Destroy()
        {
            _playerInput.Keyboard.Attack.started -= OnAttack;
        }

        private bool ReachedTarget(GameObject target)
        {
            if (target == null) return false;
            return (_player.transform.position - target.transform.position).sqrMagnitude <= SqrAttackRange;
        }
        private IEnumerable<EcsEntity> GetNearestEnemies()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                var collider = _filter.Get1(i).Value;

                // If player hits the enemy, return the target
                if (ReachedTarget(collider.gameObject))
                {
                    yield return entity;
                }
            }
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (!CanAttack) return;

            Animator.SetTrigger(ATTACK_KEY);

            foreach (var entity in GetNearestEnemies())
            {
                ref var collider = ref entity.Get<ColliderComponent>();

                // Hit the enemy
                ref var damageComponent = ref entity.Get<DamageEventComponent>();
                damageComponent.Damage = _playerModel.BaseDamage;
                damageComponent.Target = collider.Value.gameObject;
                damageComponent.Source = _player.gameObject;
            }
        }
    }
}