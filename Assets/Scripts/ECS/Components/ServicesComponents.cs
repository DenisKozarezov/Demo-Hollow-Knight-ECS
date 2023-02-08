using Entitas;
using Entitas.CodeGeneration.Attributes;
using Core.Services;

namespace Core.ECS.Components
{
    [Unique] public class Logger : IComponent, IService { public ILogService Value; }
    [Unique] public class ViewCreator : IComponent, IService { public IViewService Value; }
    [Unique] public class Time : IComponent, IService { public ITimeService Value; }
    [Unique] public class Physics : IComponent, IService { public IPhysicsService Value; }
    [Unique] public class CoroutineRunner : IComponent, IService { public ICoroutineRunnerService Value; }
    [Unique] public class CollidingViewRegister : IComponent, IService { public IRegisterService<IViewController> Value; }
    [Unique] public class Identifiers : IComponent, IService { public IIdentifierService Value; }
    [Unique, Input] public class Input : IComponent, IService { public IInputService Value; }
}