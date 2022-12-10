using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.Input;
using Core.ECS.Components;
using Core.ECS.Events.Player;

namespace Core.ECS.Systems.Player
{
    public class PlayerInteractSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _player = null;
        private readonly IInputSystem _inputSystem;

        private EcsEntity _interactableEntity;
        private InteractableComponent _interactableComponent;
        private EcsEntity _playerEntity;

        public PlayerInteractSystem(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        void IEcsInitSystem.Init()
        {
            _inputSystem.Look += OnInteract;
        }
        void IEcsDestroySystem.Destroy()
        {
            _inputSystem.Look -= OnInteract;
        }
        void IEcsRunSystem.Run()
        {
            foreach (var pl in _player)
            {
                // Player can interact with some object
                foreach (var i in _enter)
                {
                    _playerEntity = _player.GetEntity(pl);
                    _interactableEntity = _enter.Get1(i).InteractableEntity;
                    _interactableComponent = _enter.Get1(i).InteractableComponent;
                    _playerEntity.Get<CanInteractComponent>();
                }

                // Player left interactable object
                foreach (var i in _exit)
                {
                    _playerEntity.Del<CanInteractComponent>();
                }
            }
        }
        private void OnInteract()
        {
            bool canInteract = _playerEntity.Has<CanInteractComponent>() && _interactableComponent.IsInteractable;
            if (canInteract)
            {
                ExecuteInteraction(_interactableComponent.InteractType, ref _interactableEntity);
            }
        }
        private void ExecuteInteraction(InteractType type, ref EcsEntity interactableEntity)
        {
            switch (type)
            {
                case InteractType.Rest:
                    break;
                case InteractType.Read:
                    break;
                case InteractType.Talk:
                    ref var npc = ref interactableEntity.Get<NPCComponent>();
                    _world.NewEntity(new PlayerTalkingWithNPCEvent { NPC = npc });
                    break;
                case InteractType.Trade:
                    break;
            }
        }
    }
}
