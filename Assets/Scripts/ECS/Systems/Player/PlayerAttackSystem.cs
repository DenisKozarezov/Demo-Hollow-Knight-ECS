using UnityEngine;
using Leopotam.Ecs;
using Core.Input;
using Core.ECS.Events;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerAttackSystem/* : IEcsInitSystem, IEcsDestroySystem*/
    {
        //    private enum AttackDirection : byte
        //    { 
        //        None = 0x00, 
        //        Default = 0x01, 
        //        Up = 0x02, 
        //        Down = 0x03 
        //    }

        //    private readonly EcsWorld _world = null;
        //    private readonly EcsFilter<
        //        AnimatorComponent, 
        //        ColliderComponent, 
        //        DamageComponent,
        //        CanAttackComponent, 
        //        PlayerTagComponent>
        //        .Exclude<DiedComponent, ChannellingComponent> _filter = null;

        //    private readonly IInputSystem _playerInput;

        //    // ==== ANIMATIONS KEYS ===
        //    private const string DEFAULT_ATTACK_KEY = "Default Attack";
        //    private const string ATTACK_UP_KEY = "Attack Up";
        //    private const string ATTACK_DOWN_KEY = "Attack Down";
        //    // ========================

        //    private const string DefaultAttackEffectPath = "Prefabs/VFX/Default Attack";
        //    private const string AttackUpPath = "Prefabs/VFX/Attack Up";
        //    private const string AttackDownPath = "Prefabs/VFX/Attack Down";

        //    public PlayerAttackSystem(IInputSystem playerInput)
        //    {
        //        _playerInput = playerInput;
        //    }

        //    void IEcsInitSystem.Init()
        //    {
        //        _playerInput.Attack += OnAttack;
        //    }
        //    void IEcsDestroySystem.Destroy()
        //    {
        //        _playerInput.Attack -= OnAttack;
        //    }

        //    private GameObject GetEffect(AttackDirection direction, out Vector2 localPosition)
        //    {
        //        GameObject effect = null;
        //        switch (direction)
        //        {
        //            case AttackDirection.Default:
        //                effect = Resources.Load(DefaultAttackEffectPath) as GameObject;
        //                break;
        //            case AttackDirection.Up:
        //                effect = Resources.Load(AttackUpPath) as GameObject;
        //                break;
        //            case AttackDirection.Down:
        //                effect = Resources.Load(AttackDownPath) as GameObject;
        //                break;
        //        }
        //        localPosition = effect.transform.localPosition;
        //        return GameObject.Instantiate(effect);
        //    }
        //    private void OnAttack()
        //    {
        //        foreach (var i in _filter)
        //        {
        //            ref var entity = ref _filter.GetEntity(i);
        //            ref var animator = ref _filter.Get1(i).Value;
        //            ref var collider = ref _filter.Get2(i).Value;
        //            ref var damage = ref _filter.Get3(i);

        //            // Raise animator trigger
        //            GameObject attackEffect = null;
        //            if (_playerInput.Direction.y == 0f) attackEffect = CreateAttackEffect(animator);
        //            else if (_playerInput.Direction.y > 0f) attackEffect = CreateAttackEffect(animator, AttackDirection.Up);
        //            else if (_playerInput.Direction.y < 0f)
        //            {
        //                if (entity.Has<OnGroundComponent>()) continue;
        //                attackEffect = CreateAttackEffect(animator, AttackDirection.Down);
        //            }
        //            GameObject.Destroy(attackEffect, 0.15f);

        //            // Hit nearby enemies
        //            _world.NewEntity(new HitEventComponent
        //            {
        //                HitPosition = attackEffect.transform.position,
        //                HitRadius = damage.AttackRange,
        //                Damage = damage.Damage,
        //                TargetLayer = Constants.EnemyLayer,
        //                Source = animator.gameObject
        //            });
        //        }
        //    }
        //    private GameObject CreateAttackEffect(Animator animator, AttackDirection direction = AttackDirection.Default)
        //    {
        //        GameObject effect = GetEffect(direction, out Vector2 localPosition);
        //        bool flipX = animator.GetComponent<SpriteRenderer>().flipX;

        //        effect.transform.SetParent(animator.transform);
        //        effect.transform.localPosition = localPosition;

        //        switch (direction)
        //        {
        //            case AttackDirection.Default:
        //                animator.SetTrigger(DEFAULT_ATTACK_KEY);
        //                if (flipX)
        //                {
        //                    localPosition.x *= -1f;
        //                    effect.transform.localPosition = localPosition;
        //                }
        //                effect.GetComponent<SpriteRenderer>().flipY = flipX;                    
        //                break;
        //            case AttackDirection.Up:
        //                animator.SetTrigger(ATTACK_UP_KEY);
        //                break;
        //            case AttackDirection.Down:
        //                animator.SetTrigger(ATTACK_DOWN_KEY);
        //                break;
        //        }                        
        //        return effect;
        //    }
    }
}