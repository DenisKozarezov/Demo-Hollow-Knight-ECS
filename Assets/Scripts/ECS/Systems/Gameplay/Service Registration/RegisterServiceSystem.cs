using System;
using Entitas;
using Core.Services;

namespace Core.ECS.Systems
{
    public sealed class RegisterServiceSystem<TService> : IInitializeSystem
    {
        private readonly TService _service;
        private readonly Action<TService> _initServiceComponent;

        public RegisterServiceSystem(TService saveService, Action<TService> initServiceComponent)
        {
            _service = saveService;
            _initServiceComponent = initServiceComponent;
        }

        public void Initialize() => _initServiceComponent(_service);
    }
}
