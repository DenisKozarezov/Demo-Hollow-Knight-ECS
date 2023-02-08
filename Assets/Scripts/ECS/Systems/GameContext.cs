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
        public readonly UnitsModelsProvider UnitsModelsProvider;
        public readonly DiContainer DiContainer;

        public GameContext(
            EcsSystems systems, 
            IInputSystem inputSystem, 
            ICoroutineRunner coroutineRunner,
            UnitsModelsProvider modelsProvider, 
            DiContainer container)
        {
            EcsSystems = systems;
            InputSystem = inputSystem;
            CoroutineRunner = coroutineRunner;
            UnitsModelsProvider = modelsProvider;
            DiContainer = container;
        }
    }
}
