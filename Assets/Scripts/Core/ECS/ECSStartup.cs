/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Leopotam.Ecs;
using Core.Input;
using Core.Models;
using Core.ECS.Systems;
using Voody.UniLeo;
using Zenject;

namespace Core.ECS
{
    public class ECSStartup : IInitializable, IFixedTickable, ILateDisposable
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private AllSystems _allSystems;          
        private readonly IInputSystem _inputSystem;
        private readonly UnitsDefinitions _unitsDefinitions;
        private readonly DiContainer _container;

        public ECSStartup(IInputSystem inputSystem, UnitsDefinitions unitsDefinitions, DiContainer container)
        {
            _world = new EcsWorld();
            _inputSystem = inputSystem;
            _unitsDefinitions = unitsDefinitions;
            _container = container;
        }

        void IInitializable.Initialize()
        {            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world).ConvertScene();
            GameContext context = new GameContext(_systems, _inputSystem, _unitsDefinitions, _container);
            _allSystems = new AllSystems(context);            
            _allSystems.Init();
        }
        void IFixedTickable.FixedTick() 
        {
            _allSystems.Run();
        }
        void ILateDisposable.LateDispose() 
        {
            _allSystems.Destroy();
            _world.Destroy();
        }
    }
}