using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Events;
using Core.ECS.Components;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Player
{
    internal class PlayerAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<
            AnimatorComponent, 
            ColliderComponent, 
            DamageComponent,
            CanAttackComponent, 
            PlayerTagComponent>
            .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        private readonly IInputSystem _playerInput;
        private const string ATTACK_KEY = "Attack";                           

        internal PlayerAttackSystem(IInputSystem playerInput)
        {
            _playerInput = playerInput;
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
                ref var damage = ref _filter.Get3(i);

                animator.SetTrigger(ATTACK_KEY);

                // Hit nearby enemies
                ref var hitEntity = ref _world.NewEntity().Get<HitEventComponent>();
                hitEntity.HitPosition = collider.bounds.center;
                hitEntity.HitRadius = damage.AttackRange;
                hitEntity.Damage = damage.Damage;
                hitEntity.TargetLayer = Constants.EnemyLayer;
                hitEntity.Source = animator.gameObject;
            }
        }
    }
}