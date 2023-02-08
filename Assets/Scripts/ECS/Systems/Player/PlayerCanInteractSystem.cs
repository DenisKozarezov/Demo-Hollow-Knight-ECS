using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems.Player
{
    public sealed class PlayerCanInteractSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractableTriggerEnterEvent> _enter = null;
        private readonly EcsFilter<InteractableTriggerExitEvent> _exit = null;
        private readonly EcsFilter<PlayerTagComponent>.Exclude<DiedComponent> _player = null;

        void IEcsRunSystem.Run()
        {
            foreach (var pl in _player)
            {
                ref var player = ref _player.GetEntity(pl);

                // Player can interact with some object
                foreach (var i in _enter)
                {
                    ref var eventEntity = ref _enter.Get1(i);
                    ref var component = ref player.Get<CanInteractComponent>();
                    component.InteractableEntity = eventEntity.InteractableEntity;
                    component.InteractableComponent = eventEntity.InteractableComponent;
                }

                // Player left interactable object
                foreach (var i in _exit)
                {
                    player.Del<CanInteractComponent>();
                }
            }
        }
    }
}
