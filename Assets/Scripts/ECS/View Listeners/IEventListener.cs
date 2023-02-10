using Entitas;

namespace Core.ECS.ViewListeners
{
    public interface IEventListener
    {
        void RegisterListeners(IEntity entity);
        void UnregisterListeners();
    }
}
