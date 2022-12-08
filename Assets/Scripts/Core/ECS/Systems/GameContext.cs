using Core.Input;
using Core.Models;
using Leopotam.Ecs;
using Zenject;

namespace Core.ECS
{
    public sealed class GameContext
    {
        public readonly EcsSystems EcsSystems;
        public readonly IInputSystem InputSystem;
        public readonly UnitsDefinitions UnitsDefinitions;
        public readonly DiContainer DiContainer;

        public GameContext(EcsSystems systems, IInputSystem inputSystem, UnitsDefinitions unitsDefinitions, DiContainer container)
        {
            EcsSystems = systems;
            InputSystem = inputSystem;
            UnitsDefinitions = unitsDefinitions;
            DiContainer = container;
        }
    }
}
