/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Zenject;
using Core.Models;
using Core.ECS.Systems;
using Core.Services;
using Core.ECS.ViewListeners;

namespace Core.ECS
{
    public sealed class ECSStartup : ITickable
    {
        private readonly AllSystems _allSystems;

        public ECSStartup(
            IInputService inputSystem, 
            ICoroutineRunnerService coroutineRunner, 
            ILogService logger,
            IRegisterService<IViewController> collisionRegistry,
            UnitsModelsProvider modelsProvider, 
            DiContainer container)
        {
            Contexts contexts = Contexts.sharedInstance;

            Services.Services allServices = new Services.Services
            {
                InputService = inputSystem,
                CoroutineRunner = coroutineRunner,
                Logger = logger,
                CollisionRegistry = collisionRegistry
            };

            _allSystems = new AllSystems(contexts, allServices);
            _allSystems.Initialize();
        }

        public void Tick() 
        {
            _allSystems.Execute();
            _allSystems.Cleanup();
        }
    }
}