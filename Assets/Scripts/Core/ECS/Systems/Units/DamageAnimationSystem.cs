using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Events;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class DamageAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimateDamageEventComponent>.Exclude<DiedComponent> _filter = null;

        private const float Duration = 0.4f;
        private Color WhiteColor = new Color(1, 1, 1, 0.7f);

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var eventComponent = ref entity.Get<AnimateDamageEventComponent>();

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
                    if (eventComponent.Duration < 0f)
                    {
                        renderer.color = Color.black;
                        entity.Del<AnimateDamageEventComponent>();
                    }
                }
            }
        }
    }
}