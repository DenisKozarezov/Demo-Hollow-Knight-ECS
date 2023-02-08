using Leopotam.Ecs;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;
using Core.ECS.Events.Player;
using Core.Input;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerInteractingSystem/* : IEcsInitSystem, IEcsDestroySystem*/
    {
        //private readonly EcsWorld _world = null;
        //private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _player = null;
        //private readonly IInputSystem _inputSystem;
        //private EcsEntity _playerEntity;

        //public PlayerInteractingSystem(IInputSystem inputSystem)
        //{
        //    _inputSystem = inputSystem;
        //}

        //void IEcsInitSystem.Init()
        //{
        //    _playerEntity = _player.GetEntity(0);
        //    _inputSystem.Look += OnInteract;
        //}
        //void IEcsDestroySystem.Destroy()
        //{
        //    _inputSystem.Look -= OnInteract;
        //}
        //private void OnInteract()
        //{
        //    if (_playerEntity.IsNullOrEmpty() || !_playerEntity.Has<CanInteractComponent>()) return;

        //    ref var component = ref _playerEntity.Get<CanInteractComponent>();
            
        //    // If object is open for interaction
        //    bool canInteract = component.InteractableComponent.IsInteractable;

        //    if (canInteract)
        //    {
        //        InteractType interactionType = component.InteractableComponent.InteractType;
        //        ExecuteInteraction(interactionType, ref component.InteractableEntity);
        //    }
        //}
        //private void ExecuteInteraction(InteractType type, ref EcsEntity interactableEntity)
        //{
        //    switch (type)
        //    {
        //        case InteractType.Rest:
        //            break;
        //        case InteractType.Read:
        //            break;
        //        case InteractType.Talk:
        //            ref var npc = ref interactableEntity.Get<NPCComponent>();
        //            _world.NewEntity(new PlayerTalkingWithNPCEvent { NPC = npc });
        //            break;
        //        case InteractType.Trade:
        //            break;
        //    }
        //}
    }
}
