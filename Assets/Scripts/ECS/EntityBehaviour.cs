using UnityEngine;
using Core.ECS.ViewListeners;

namespace Core.ECS
{
    public class EntityBehaviour : MonoBehaviour
    {
        protected GameEntity Entity { get; private set; }
        protected GameContext Game { get; private set; }

        protected virtual void Awake()
        {
            Game = Contexts.sharedInstance.game;
            Entity = Game.CreateEntity();

            var viewController = gameObject.AddComponent<UnityViewController>();
            viewController.InitializeView(Contexts.sharedInstance.game, Entity);

            foreach (var listener in GetComponents<IEventListener>())
            {
                listener.RegisterListeners(Entity);
            }
        }
        protected virtual void Start() { }
        protected virtual void OnDestroy() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}
