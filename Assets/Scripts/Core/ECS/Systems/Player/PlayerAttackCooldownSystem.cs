using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Player
{
    public class PlayerAttackCooldownSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<AttackCooldownComponent, PlayerTagComponent>.Exclude<DiedComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private bool _canAttack = true;
        private float _timer;

        public PlayerAttackCooldownSystem(IInputSystem playerInput)
        {
            _playerInput = playerInput;
        }

        private void OnAttack()
        {
            if (_canAttack) _canAttack = false;
        }
        void IEcsInitSystem.Init()
        {
            _playerInput.Attack += OnAttack;
        }
        void IEcsDestroySystem.Destroy()
        {
            _playerInput.Attack -= OnAttack;
        }
        void IEcsRunSystem.Run()
        {
            if (_canAttack) return;

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);             
         
                if (entity.Has<CanAttackComponent>())
                {
                    _timer = entity.Get<AttackCooldownComponent>().Value;
                    entity.Del<CanAttackComponent>();
                }

                if (_timer > 0f) _timer -= Time.deltaTime;
                else
                {
                    entity.Get<CanAttackComponent>();
                    _canAttack = true;
                }
            }
        }
    }
}