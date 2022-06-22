using Core.Input;
using Core.Models;
using Leopotam.Ecs;

namespace Core.ECS
{
    internal sealed class GameContext
    {
        public readonly EcsSystems EcsSystems;
        public readonly IInputSystem InputSystem;
        public readonly UnitsDefinitions UnitsDefinitions;

        internal GameContext(EcsSystems systems, IInputSystem inputSystem, UnitsDefinitions unitsDefinitions)
        {
            EcsSystems = systems;
            InputSystem = inputSystem;
            UnitsDefinitions = unitsDefinitions;
        }
    }
}
