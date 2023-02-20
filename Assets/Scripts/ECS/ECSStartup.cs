/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using Zenject;
using Core.ECS.Systems;
using Core.ECS.ViewListeners;
using Core.Services;

namespace Core.ECS
{
    public sealed class ECSStartup : IInitializable, ITickable
    {
        private readonly AllSystems _allSystems;

        public ECSStartup(
            IInputService inputSystem, 
            ICoroutineRunnerService coroutineRunner, 
            ILogService logger,
            IRegisterService<IViewController> collisionRegistry,
            ITimeService time,
            IPhysicsService physics,
            IIdentifierService identifier,
            DiContainer diContainer)
        {
            Contexts contexts = Contexts.sharedInstance;

            Services.Services allServices = new Services.Services
            {
                Logger = logger,
                Identifiers = identifier,
                Time = time,
                InputService = inputSystem,
                Physics = physics,
                CoroutineRunner = coroutineRunner,
                CollisionRegistry = collisionRegistry,
                DiContainer = diContainer
            };

            _allSystems = new AllSystems(contexts, allServices);
            _allSystems.Initialize();
        }

        public void Initialize()
        {
            ECSExtensions.Empty().AddCameraFade(newFadeTime: 3f, newFadeMode: FadeMode.Off);
        }
        public void Tick() 
        {
            _allSystems.Execute();
            _allSystems.Cleanup();
        }
    }
}