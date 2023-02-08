using Core.Services;

namespace Core.ECS.Systems
{
    public sealed class ServiceRegistrationSystems : Feature
    {
        public ServiceRegistrationSystems(Contexts contexts, Services.Services services)
          : base(nameof(ServiceRegistrationSystems))
        {
            GameContext game = contexts.game;
            InputContext input = contexts.input;

            Add(new RegisterServiceSystem<ILogService>(services.Logger, game.ReplaceLogger));
            Add(new RegisterServiceSystem<IViewService>(services.ViewService, game.ReplaceViewCreator));
            Add(new RegisterServiceSystem<ITimeService>(services.Time, game.ReplaceTime));
            Add(new RegisterServiceSystem<IInputService>(services.InputService, input.ReplaceInput));
            Add(new RegisterServiceSystem<IPhysicsService>(services.Physics, game.ReplacePhysics));
            Add(new RegisterServiceSystem<ICoroutineRunnerService>(services.CoroutineDirector, game.ReplaceCoroutineRunner));
            Add(new RegisterServiceSystem<IRegisterService<IViewController>>(services.CollidingViewRegister, game.ReplaceCollidingViewRegister));
            Add(new RegisterServiceSystem<IIdentifierService>(services.Identifiers, game.ReplaceIdentifiers));
        }
    }
}
