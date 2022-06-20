using UnityEngine;
using Leopotam.Ecs;
using Core.Models;
using Core.Input;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAttackCooldownSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _filter = null;

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
            if (_canAttack) _canAttack = false;
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

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                if (entity.Has<CanAttackComponent>()) entity.Del<CanAttackComponent>();

                if (_timer > 0f) _timer -= Time.deltaTime;
                else
                {
                    entity.Get<CanAttackComponent>();
                    _canAttack = true;
                    _timer = _playerModel.AttackCooldown;
                }
            }
        }
    }
}