using Leopotam.Ecs;
using Core.Models;
using Core.Input;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<
            AnimatorComponent, 
            ColliderComponent, 
            CanAttackComponent, 
            PlayerTagComponent>
            .Exclude<DiedComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private readonly PlayerModel _playerModel;
        private const string ATTACK_KEY = "Attack";                           

        internal PlayerAttackSystem(IInputSystem playerInput, PlayerModel playerModel)
        {
            _playerInput = playerInput;
            _playerModel = playerModel;
        }

        public void Init()
        {
            _playerInput.Attack += OnAttack;
        }
        public void Destroy()
        {
            _playerInput.Attack -= OnAttack;
        }
        private void OnAttack()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get1(i).Value;
                ref var collider = ref _filter.Get2(i).Value;

                animator.SetTrigger(ATTACK_KEY);

                // Hit nearby enemies
                ref var hitEntity = ref _world.NewEntity().Get<HitEventComponent>();
                hitEntity.HitPosition = collider.bounds.center;
                hitEntity.HitRadius = _playerModel.AttackRange;
                hitEntity.Damage = _playerModel.BaseDamage;
                hitEntity.TargetLayer = Constants.EnemyLayer;
                hitEntity.Source = animator.gameObject;
            }
        }
    }
}