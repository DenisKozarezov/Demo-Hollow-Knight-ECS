/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Leopotam.Ecs;
using Core.Input;
using Core.Models;
using Core.ECS.Systems;
using Zenject;

namespace Core.ECS
{
    public class ECSStartup : IInitializable, IFixedTickable, ILateDisposable
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private AllSystems _allSystems;          
        private readonly IInputSystem _inputSystem;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly UnitsModelsProvider _modelsProvider;
        private readonly DiContainer _container;

        public ECSStartup(IInputSystem inputSystem, ICoroutineRunner coroutineRunner, UnitsModelsProvider modelsProvider, DiContainer container)
        {
            _world = new EcsWorld();
            _inputSystem = inputSystem;
            _coroutineRunner = coroutineRunner;
            _modelsProvider = modelsProvider;
            _container = container;
        }

        void IInitializable.Initialize()
        {      
            Contexts contexts = Contexts.sharedInstance;

            _allSystems = new AllSystems(contexts);            
            _allSystems.Initialize();
        }
        void IFixedTickable.FixedTick() 
        {
            _allSystems.Execute();
        }
        void ILateDisposable.LateDispose() 
        {
            _allSystems.Cleanup();
        }
    }
}