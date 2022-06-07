using UnityEngine;
using Leopotam.Ecs;
using Examples.Example_1.ECS.Events;

namespace Examples.Example_1.ECS.Systems
{
    internal sealed class DamageAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimateDamageEventComponent> _filter = null;

        private float TimeAliveSeconds = 0.7f;
        private Color RedColor = new Color(10, 0, 0, 1);

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var eventComponent = ref entity.Get<AnimateDamageEventComponent>();

                SpriteRenderer renderer = eventComponent.GameObjectRef.GetComponent<SpriteRenderer>();

                // Colorize in red
                if (eventComponent.Damaged == false)
                {
                    eventComponent.ColorRef = renderer.color;
                    renderer.color = RedColor;
                    eventComponent.Damaged = true;
                    eventComponent.TimeAlive = 0;
                }

                // Return default color
                else
                {
                    eventComponent.TimeAlive += Time.deltaTime;
                    if (eventComponent.TimeAlive >= TimeAliveSeconds)
                    {
                        renderer.color = eventComponent.ColorRef;
                        entity.Del<AnimateDamageEventComponent>();
                    }
                }
            }
        }
    }
}