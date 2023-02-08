using System;
using Core.ECS.Components.Player;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems
{
    public sealed class EnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnergyReducedEvent> _event = null;
        private readonly EcsFilter<EnergyComponent> _filter = null;

        public void Run()
        {
            foreach (var @event in _event)
            {
                foreach (var i in _filter)
                {
                    ref var reducedEnergy = ref _event.Get1(i);
                    ref var currentEnergy = ref _filter.Get1(i);

                    // Reduce energy
                    currentEnergy.Energy = Math.Max(currentEnergy.Energy - reducedEnergy.Value, 0f);
                }
            }
        }
    }
}
