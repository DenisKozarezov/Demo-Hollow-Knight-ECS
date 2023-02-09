using UnityEngine;
using Entitas;
using Entitas.VisualDebugging.Unity;
using Core.ECS.ViewComponentRegistrators;

namespace Core.ECS.ViewListeners
{
    public sealed class UnityViewController : MonoBehaviour, IViewController
    {
        private GameContext _game;
        public GameEntity Entity { get; private set; }

        public IViewController InitializeView(GameContext game, IEntity entity)
        {
            _game = game;
            Entity = (GameEntity)entity;

            Entity.AddViewController(this);

            AddDestroyedListener();

            RegisterViewComponents();

            return this;
        }

        public void Destroy()
        {
            UnregisterCollisions();
            UnregisterListeners(Entity);
            gameObject.DestroyGameObject();
        }

        private void Start()
        {
            RegisterCollisions();
            //Entity.WithNewGeneralId();
        }

        private void RegisterViewComponents()
        {
            foreach (IViewComponentRegistrator registrator in GetComponents<IViewComponentRegistrator>())
                registrator.Register(Entity);

            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) Entity.AddSpriteRenderer(spriteRenderer);
        }
        private void UnregisterListeners(IEntity entity)
        {
            foreach (IEventListener listener in GetComponents<IEventListener>())
                listener.UnregisterListeners(entity);
        }
        private void AddDestroyedListener()
        {
            if (!GetComponent<DestroyedListener>()) 
                gameObject.AddComponent<DestroyedListener>();
        }

        private void RegisterCollisions()
        {
            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>(includeInactive: true))
                _game.collisionRegistry.Value.Register(collider.GetInstanceID(), this);
        }
        private void UnregisterCollisions()
        {
            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
                _game.collisionRegistry.Value.Unregister(collider.GetInstanceID(), this);
        }
    }
}
