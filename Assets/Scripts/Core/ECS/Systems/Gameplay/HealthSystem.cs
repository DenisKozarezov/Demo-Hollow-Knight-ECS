using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;

namespace Core.ECS.Systems
{
    internal sealed class HealthSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, AnimatorComponent>.Exclude<DiedComponent> _filter = null;

        private const string DEATH_KEY = "Death";

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var healthComponent = ref _filter.Get1(i);
                ref var animatorComponent = ref _filter.Get2(i);

                if (healthComponent.Health <= 0)
                {
                    // Play dead
                    animatorComponent.Value.SetTrigger(DEATH_KEY);

#if UNITY_EDITOR
                    Debug.Log($"Unit <b><color=yellow>{animatorComponent.Value.name}</color></b> <b><color=red>died</color></b>.");
#endif

                    // Add died component
                    entity.Get<DiedComponent>();
                }
            }
        }
    }
}
