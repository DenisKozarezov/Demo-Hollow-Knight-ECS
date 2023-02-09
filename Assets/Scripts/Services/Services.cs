using Core.ECS.ViewListeners;

namespace Core.Services
{
    public struct Services
    {
        public ILogService Logger;
        //public IIdentifierService Identifiers;
        //public IViewService ViewService;
        public ITimeService Time;
        public IInputService InputService;
        public IPhysicsService Physics;
        public ICoroutineRunnerService CoroutineRunner;
        public IRegisterService<IViewController> CollisionRegistry;
    }
}
