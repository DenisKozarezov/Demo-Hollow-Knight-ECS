using UnityEngine;
using Leopotam.Ecs;
using Core.Units;
using Core.Models;
using Core.Input;
using Core.ECS.Events;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;

        private readonly IInputSystem _playerInput;
        private readonly UnitScript _player = null;
        private readonly PlayerModel _playerModel;
        private const string ATTACK_KEY = "Attack";      
                     
        private Animator Animator;    

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

        private void OnAttack()
        {
            if (!CanAttack) return;

            Animator.SetTrigger(ATTACK_KEY);

            ref var hitEntity = ref _world.NewEntity().Get<HitEventComponent>();
            hitEntity.HitPosition = _player.transform.position;
            hitEntity.HitRadius = _playerModel.AttackRange;
            hitEntity.Damage = _playerModel.BaseDamage;
            hitEntity.TargetLayer = Constants.EnemyLayer;
            hitEntity.Source = _player.gameObject;
        }
    }
}