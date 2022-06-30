using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.UI;
using Core.Input;
using Core.ECS.Events.Player;
using AI.ECS;

namespace Core.ECS.Systems.Player
{
    internal class PlayerInteractSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _player = null;
        private readonly IInputSystem _inputSystem;
        private InteractableView _view;

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

                // Player can interact with something
                foreach (var i in _enter)
                {
                    SetInteractable(ref entity, true, _enter.Get1(i).View);
                }

                // Player left interactable object
                foreach (var i in _exit)
                {
                    SetInteractable(ref entity, false);
                }
            }
        }
        private void SetInteractable(ref EcsEntity player, bool isInteractable, InteractableView view = null)
        {
            if (isInteractable) player.Get<CanInteractComponent>().View = view;
            else player.Del<CanInteractComponent>();
            _view = view;
        }
        private void OnInteract()
        {
            if (_view == null) return;

            foreach (var i in _player)
            {
                ref var entity = ref _player.GetEntity(i);
                if (entity.Has<CanInteractComponent>())
                {
                    ExecuteInteraction(_view);
#if UNITY_EDITOR
                    Debug.Log($"Player interacting with <b><color=yellow>{_view.name}</color></b>. Interaction type: <b><color=green>{_view.InteractionType}</color></b>.");
#endif
                }
            }
        }
        private void ExecuteInteraction(InteractableView view)
        {
            switch (view.InteractionType)
            {
                case InteractType.Rest:
                    break;
                case InteractType.Read:
                    break;
                case InteractType.Talk:
                    ref var npc = ref _view.GetComponent<EntityReference>().Entity.Get<NPCComponent>();
                    _world.NewEntity(new PlayerTalkingWithNPCEvent { NPC = npc });
                    break;
                case InteractType.Trade:
                    break;
            }
        }
    }
}
