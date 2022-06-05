using Examples.Example_1.ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.ECS.Systems
{
    public class DamageAnimationSystem : IEcsRunSystem
    {
        protected EcsWorld _world = null; // Переменная _world автоматически инициализируется
        protected EcsFilter<AnimateDamageEventComponent> _filter = null;

        private float TimeAliveSeconds = 0.7f;
        private Color RedColor = new Color(10, 0, 0, 1);

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity (i);
                ref var eventComponent = ref ecsEntity.Get<AnimateDamageEventComponent>();

                SpriteRenderer renderer = eventComponent.GameObjectRef.GetComponent<SpriteRenderer>();

                // Colorize in red
                if (eventComponent.Damaged == false)
                {
                    eventComponent.ColorRef = ecsEntity.Get<AnimateDamageEventComponent>().GameObjectRef.GetComponent<SpriteRenderer>().color;
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
                        ecsEntity.Del<AnimateDamageEventComponent>();
                    }
                }
            }
        }
    }
}