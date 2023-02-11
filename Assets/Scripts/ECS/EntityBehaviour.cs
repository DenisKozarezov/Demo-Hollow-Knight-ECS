using UnityEngine;
using Core.ECS.ViewListeners;

namespace Core.ECS
{
    public class EntityBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool _selfInitialize;

        private ViewController _viewController;

        protected GameEntity Entity { get; private set; }
        protected GameContext Game => Contexts.sharedInstance.game;

        protected virtual void Awake()
        {
            _viewController = GetComponent<ViewController>();

            if (_selfInitialize) Entity = Game.CreateEntity();

            if (_viewController)
            {
                _viewController.InitializeView(Game, Entity);
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
