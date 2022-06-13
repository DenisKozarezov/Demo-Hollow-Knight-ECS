using UnityEngine;
using UnityEngine.InputSystem;
using Leopotam.Ecs;
using Core.Models;
using Core.Input;
using Examples.Example_1.ECS.Components.Player;

namespace Examples.Example_1.ECS.Systems.Player
{
    internal class PlayerAttackCooldownSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<CanAttackComponent, PlayerTagComponent>
            .Exclude<DiedComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private readonly PlayerModel _playerModel;
        private float _timer;
        private bool _canAttack = true;

        public PlayerAttackCooldownSystem(IInputSystem playerInput, PlayerModel playerModel)
        {
            _playerInput = playerInput;
            _playerModel = playerModel;
            _timer = _playerModel.AttackCooldown;
        }

        private void OnAttack()
        {
            if (_canAttack) SetAttack(false);
        }
        private void SetAttack(bool canAttack)
        {
            _canAttack = canAttack;
            var entity = _filter.GetEntity(0);
            if (canAttack) entity.Get<CanAttackComponent>();
            else entity.Del<CanAttackComponent>();
        }

        public void Init()
        {
            _playerInput.Attack += OnAttack;
        }
        public void Destroy()
        {
            _playerInput.Attack -= OnAttack;
        }
        public void Run()
        {
            if (_canAttack) return;

            if (_timer > 0f) _timer -= Time.deltaTime;
            else 
            {
                SetAttack(true);
                _timer = _playerModel.AttackCooldown;
            }
        }
    }
}