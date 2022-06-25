using System.Collections;
using UnityEngine;
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
        private enum AttackDirection { None, Default, Up, Down }

        private readonly EcsWorld _world = null;
        private readonly EcsFilter<
            AnimatorComponent, 
            ColliderComponent, 
            DamageComponent,
            CanAttackComponent, 
            PlayerTagComponent>
            .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        private readonly IInputSystem _playerInput;

        // ==== ANIMATIONS KEYS ===
        private const string DEFAULT_ATTACK_KEY = "Default Attack";
        private const string ATTACK_UP_KEY = "Attack Up";
        private const string ATTACK_DOWN_KEY = "Attack Down";
        // ========================

        private const string DefaultAttackEffectPath = "Prefabs/Effects/Default Attack";
        private const string AttackUpPath = "Prefabs/Effects/Attack Up";
        private const string AttackDownPath = "Prefabs/Effects/Attack Down";

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

        private GameObject GetEffect(AttackDirection direction, out Vector2 localPosition)
        {
            GameObject effect = null;
            switch (direction)
            {
                case AttackDirection.Default:
                    effect = Resources.Load(DefaultAttackEffectPath) as GameObject;
                    break;
                case AttackDirection.Up:
                    effect = Resources.Load(AttackUpPath) as GameObject;
                    break;
                case AttackDirection.Down:
                    effect = Resources.Load(AttackDownPath) as GameObject;
                    break;
            }
            localPosition = effect.transform.localPosition;
            return GameObject.Instantiate(effect);
        }
        private void OnAttack()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animator = ref _filter.Get1(i).Value;
                ref var collider = ref _filter.Get2(i).Value;
                ref var damage = ref _filter.Get3(i);

                // Raise animator trigger
                GameObject attackEffect = null;
                if (_playerInput.Direction.y == 0f) attackEffect = CreateAttackEffect(animator);
                else if (_playerInput.Direction.y > 0f) attackEffect = CreateAttackEffect(animator, AttackDirection.Up);
                else if (_playerInput.Direction.y < 0f)
                {
                    if (entity.Has<OnGroundComponent>()) continue;
                    attackEffect = CreateAttackEffect(animator, AttackDirection.Down);
                }

                // Hit nearby enemies
                ref var hitEntity = ref _world.NewEntity().Get<HitEventComponent>();
                hitEntity.HitPosition = attackEffect.transform.position;
                hitEntity.HitRadius = damage.AttackRange;
                hitEntity.Damage = damage.Damage;
                hitEntity.TargetLayer = Constants.EnemyLayer;
                hitEntity.Source = animator.gameObject;
            }
        }
        private GameObject CreateAttackEffect(Animator animator, AttackDirection direction = AttackDirection.Default)
        {
            GameObject effect = GetEffect(direction, out Vector2 localPosition);
            bool flipX = animator.GetComponent<SpriteRenderer>().flipX;

            effect.transform.SetParent(animator.transform);
            effect.transform.localPosition = localPosition;

            switch (direction)
            {
                case AttackDirection.Default:
                    animator.SetTrigger(DEFAULT_ATTACK_KEY);
                    if (flipX) effect.transform.localPosition *= -1;
                    effect.GetComponent<SpriteRenderer>().flipY = flipX;
                    break;
                case AttackDirection.Up:
                    animator.SetTrigger(ATTACK_UP_KEY);
                    break;
                case AttackDirection.Down:
                    animator.SetTrigger(ATTACK_DOWN_KEY);
                    break;
            }

            animator.GetComponent<MonoBehaviour>().StartCoroutine(AttackCoroutine(effect));
            return effect;
        }
        private IEnumerator AttackCoroutine(GameObject effect)
        {
            yield return new WaitForSeconds(0.15f);
            GameObject.Destroy(effect);
        }
    }
}