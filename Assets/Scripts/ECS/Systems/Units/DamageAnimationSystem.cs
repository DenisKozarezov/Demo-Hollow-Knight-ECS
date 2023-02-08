using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;

namespace Core.ECS.Systems
{
    public sealed class DamageAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimateDamageEventComponent> _event = null;
        private const float Duration = 0.4f;
        private Color WhiteColor = new Color(1, 1, 1, 0.7f);

        void IEcsRunSystem.Run()
        {
            foreach (var @event in _event)
            {
                ref var entity = ref _event.GetEntity(@event);
                ref var eventComponent = ref _event.Get1(@event);

                SpriteRenderer renderer = eventComponent.GameObjectRef.GetComponent<SpriteRenderer>();

                // Colorize in white
                if (!eventComponent.Damaged)
                {
                    renderer.color = WhiteColor;
                    eventComponent.Damaged = true;
                    eventComponent.Duration = Duration;
                }

                // Return default color
                else
                {
                    eventComponent.Duration -= Time.deltaTime;
                    if (eventComponent.Duration <= 0f)
                    {
                        renderer.color = Color.black;
                        entity.Del<AnimateDamageEventComponent>();
                    }
                }
            }
        }
    }
}