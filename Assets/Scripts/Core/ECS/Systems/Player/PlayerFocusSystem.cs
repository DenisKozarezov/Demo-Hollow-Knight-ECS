using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events.Player;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.Input;
using Core.Models;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerFocusSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AnimatorComponent, HealthComponent, PlayerTagComponent>
          .Exclude<DiedComponent> _filter = null;
        private readonly IInputSystem _playerInput;
        private readonly HealingFocusAbility _focusAbility;

        private bool _focusing;
        private float _timer;
        private const string FOCUS_KEY = "Is Focusing";

        public PlayerFocusSystem(IInputSystem playerInput, HealingFocusAbility focusAbility)
        {
            _playerInput = playerInput;
            _focusAbility = focusAbility;
        }

        void IEcsInitSystem.Init()
        {
            _playerInput.FocusStarted += OnFocusStarted;
            _playerInput.FocusCanceled += OnFocusCancelled;
        }
        void IEcsDestroySystem.Destroy()
        {
            _playerInput.FocusStarted -= OnFocusStarted;
            _playerInput.FocusCanceled -= OnFocusCancelled;
        }

        private void OnFocusStarted()
        {
            _focusing = true;
            foreach (var i in _filter)
            {
                var animator = _filter.Get1(i).Value;
                animator.SetBool(FOCUS_KEY, true);
            }
        }
        private void OnFocusCancelled()
        {
            foreach (var i in _filter) Reset(ref _filter.GetEntity(i));
        }
        private void Reset(ref EcsEntity entity)
        {
            _focusing = false;
            _timer = 0f;
            entity.Get<AnimatorComponent>().Value.SetBool(FOCUS_KEY, false);
            entity.Del<ChannellingComponent>();
        }
        void IEcsRunSystem.Run()
        {
            if (!_focusing || _timer > _focusAbility.HoldTime) return;

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var health = ref _filter.Get2(i);

                // If full HP
                if (health.Health >= health.MaxHealth) continue;

                entity.Get<ChannellingComponent>();

                _timer += Time.deltaTime;
                if (_timer >= _focusAbility.HoldTime)
                {                    
                    Reset(ref entity);

                    // Reduce energy
                    _world.NewEntity(new EnergyReducedEvent { Value = _focusAbility.EnergyCost });

                    // Heal
                    _world.NewEntity(new PlayerHealedEvent { Value = _focusAbility.HealthRestore });
                }
            }
        }
    }
}
