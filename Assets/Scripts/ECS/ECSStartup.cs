/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Zenject;
using Core.Models;
using Core.ECS.Systems;
using Core.Services;

namespace Core.ECS
{
    public sealed class ECSStartup : IInitializable, IFixedTickable
    {
        private readonly AllSystems _allSystems;

        public ECSStartup(IInputService inputSystem, ICoroutineRunnerService coroutineRunner, UnitsModelsProvider modelsProvider, DiContainer container)
        {
            Contexts contexts = Contexts.sharedInstance;

            Services.Services allServices = new Services.Services
            {
                InputService = inputSystem,
                CoroutineRunner = coroutineRunner,
            };

            _allSystems = new AllSystems(contexts, allServices);
        }

        public void Initialize()
        {            
            _allSystems.Initialize();
        }
        public void FixedTick() 
        {
            _allSystems.Execute();
            _allSystems.Cleanup();
        }
    }
}