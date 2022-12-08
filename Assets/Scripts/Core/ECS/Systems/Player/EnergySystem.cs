using System;
using Core.ECS.Components.Player;
using Core.ECS.Events.Player;
using Leopotam.Ecs;

namespace Core.ECS.Systems
{
    public class EnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnergyComponent, EnergyReducedEvent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var energyComponent = ref _filter.Get1(i);
                ref var energyEvent = ref _filter.Get2(i);

                // Reduce energy
                energyComponent.Energy = Math.Max(energyComponent.Energy - energyEvent.Value, 0f);     
            }
        }
    }
}
