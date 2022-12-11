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
            _world = new EcsWorld();
            _systems = new EcsSystems(_world).ConvertScene();
            GameContext context = new GameContext(_systems, _inputSystem, _coroutineRunner, _modelsProvider, _container);
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