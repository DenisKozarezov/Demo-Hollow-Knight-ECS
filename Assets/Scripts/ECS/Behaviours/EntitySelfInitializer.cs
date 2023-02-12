using Core.ECS.ViewListeners;

namespace Core.ECS.Behaviours
{
    public sealed class EntitySelfInitializer : EntityBehaviour
    {
        protected override void Awake()
        {
            GameEntity entity = Game.CreateEntity();

            ViewController viewController = gameObject.AddComponent<ViewController>();
            if (viewController != null)
            {
                viewController.InitializeView(Game, entity);
            }

            foreach (var listener in GetComponents<IEventListener>())
            {
                if (listener.GetType() == this.GetType()) continue;

                listener.RegisterListeners(entity);
            }

            Destroy(this);
        }
    }
}
