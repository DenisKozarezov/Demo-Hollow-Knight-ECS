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
        public readonly ICoroutineRunner CoroutineRunner;
        public readonly UnitsDefinitions UnitsDefinitions;
        public readonly DiContainer DiContainer;

        public GameContext(
            EcsSystems systems, 
            IInputSystem inputSystem, 
            ICoroutineRunner coroutineRunner,
            UnitsDefinitions unitsDefinitions, 
            DiContainer container)
        {
            EcsSystems = systems;
            InputSystem = inputSystem;
            CoroutineRunner = coroutineRunner;
            UnitsDefinitions = unitsDefinitions;
            DiContainer = container;
        }
    }
}
