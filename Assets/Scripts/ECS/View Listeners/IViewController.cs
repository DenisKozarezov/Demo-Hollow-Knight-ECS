using Entitas;

namespace Core.ECS.ViewListeners
{
    public interface IViewController
    {
        GameEntity Entity { get; }
        IViewController InitializeView(GameContext game, IEntity entity);
        void Destroy();
    }
}
