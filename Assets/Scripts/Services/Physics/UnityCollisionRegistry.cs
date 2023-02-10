using System.Collections.Generic;
using Core.ECS.ViewListeners;

namespace Core.Services
{
    public sealed class UnityCollisionRegistry : IRegisterService<IViewController>
    {
        private readonly Dictionary<int, IViewController> _allControllersById = new();

        public IViewController Register(int instanceId, IViewController viewController)
        {
            _allControllersById.TryAdd(instanceId, viewController);
            return viewController;
        }
        public void Unregister(int instanceId, IViewController @object)
        {
            if (_allControllersById.ContainsKey(instanceId))
                _allControllersById.Remove(instanceId);
        }
        public IViewController Take(int key)
        {
            if (_allControllersById.TryGetValue(key, out IViewController behaviour))
            {
                return behaviour;
            }
            return null;
        }
    }
}
