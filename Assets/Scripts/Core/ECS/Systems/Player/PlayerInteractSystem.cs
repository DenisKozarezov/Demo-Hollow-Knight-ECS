using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.UI;
using Core.Input;
using UnityEngine;

namespace Core.ECS.Systems.Player
{
    internal class PlayerInteractSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _player = null;
        private readonly IInputSystem _inputSystem;
        private InteractableView _view;
        private EcsEntity _entity;

        internal PlayerInteractSystem(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        public void Init()
        {
            _inputSystem.Look += OnInteract;
        }
        public void Destroy()
        {
            _inputSystem.Look -= OnInteract;
        }
        public void Run()
        {
            foreach (var player in _player)
            {
                ref var entity = ref _player.GetEntity(player);
                _entity = entity;

                // Player can interact with something
                foreach (var i in _enter)
                {
                    ref var view = ref _enter.Get1(i).View;
                    SetInteractable(ref entity, true, view);
                }

                // Player left interactable object
                foreach (var i in _exit)
                {
                    ref var view = ref _enter.Get1(i).View;
                    SetInteractable(ref entity, false);
                }
            }
        }
        private void SetInteractable(ref EcsEntity player, bool isInteractable, InteractableView view = null)
        {
            if (isInteractable)
            {
                player.Get<CanInteractComponent>().View = view;
                _view = view;
            }
            else
            {
                player.Del<CanInteractComponent>();
                _view = null;
            }
        }
        private void OnInteract()
        {
            if (!_entity.Has<CanInteractComponent>()) return;
            
            _entity.Del<CanInteractComponent>();

#if UNITY_EDITOR
            Debug.Log($"Player interacting with <b><color=yellow>{_view.name}</color></b>. Interaction type: <b><color=green>{_view.InteractionType}</color></b>.");
#endif
        }
    }
}
