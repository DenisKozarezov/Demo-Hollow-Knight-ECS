using Entitas;
using Entitas.CodeGeneration.Attributes;
using Core.Services;
using Core.ECS.ViewListeners;

namespace Core.ECS.Components
{
    [Unique] public sealed class Logger : IComponent, IService { public ILogService Value; }
    //[Unique] public sealed class ViewCreator : IComponent, IService { public IViewService Value; }
    [Unique] public sealed class Time : IComponent, IService { public ITimeService Value; }
    [Unique] public sealed class Physics : IComponent, IService { public IPhysicsService Value; }
    [Unique] public sealed class CoroutineRunner : IComponent, IService { public ICoroutineRunnerService Value; }
    [Unique] public sealed class CollisionRegistry : IComponent, IService { public IRegisterService<IViewController> Value; }
    [Unique] public sealed class Identifiers : IComponent, IService { public IIdentifierService Value; }
    [Unique, Input] public sealed class Input : IComponent, IService { public IInputService Value; }
}