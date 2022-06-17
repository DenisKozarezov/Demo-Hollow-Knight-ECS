using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Core.Units;
using Core.Models;
using Core.Input;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<ColliderComponent, HealthComponent, HittableComponent>
            .Exclude<PlayerTagComponent, DiedComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private readonly UnitScript _player = null;
        private readonly PlayerModel _playerModel;
        private const string ATTACK_KEY = "Attack";      
                     
        private Animator Animator;    

        private float SqrAttackRange => _playerModel.AttackRange * _playerModel.AttackRange;
        private bool CanAttack => _player.EntityReference.Entity.Has<CanAttackComponent>();

        internal PlayerAttackSystem(IInputSystem playerInput, PlayerModel playerModel)
        {
            _playerInput = playerInput;
            _playerModel = playerModel;
        }

        public void Init()
        {
            _playerInput.Attack += OnAttack;
            Animator = _player.GetComponent<Animator>();
        }
        public void Destroy()
        {
            _playerInput.Attack -= OnAttack;
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

        private void OnAttack()
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