using UnityEngine;
using Core.ECS.ViewListeners;

namespace Core.ECS
{
    public class EntityBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool _selfInitialize;

        protected GameEntity Entity { get; private set; }
        protected GameContext Game => Contexts.sharedInstance.game;

        protected virtual void Awake()
        {
            if (_selfInitialize)
            {
                Entity = Game.CreateEntity();
            }

            var viewController = GetComponent<ViewController>();
            if (viewController)
            {
                viewController.InitializeView(Game, Entity);
            }

            foreach (var listener in GetComponents<IEventListener>())
            {
                if (listener.GetType() == this.GetType()) continue;

                listener.RegisterListeners(Entity);
            }
        }
        protected virtual void Start() { }
        protected virtual void OnDestroy() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}
