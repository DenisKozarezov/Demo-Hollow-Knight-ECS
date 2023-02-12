using System;
using UnityEngine;
using Entitas;
using Core.ECS.ViewComponentRegistrators;

namespace Core.ECS.ViewListeners
{
    public sealed class ViewController : MonoBehaviour, IViewController, IDisposable
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

        private void Start()
        {
            Entity.AddId(_game.identifiers.Value.Next());
            RegisterCollisions();
        }

        private void RegisterViewComponents()
        {
            foreach (IViewComponentRegistrator registrator in GetComponents<IViewComponentRegistrator>())
                registrator.Register(Entity);

            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) Entity.AddSpriteRenderer(spriteRenderer);
        }
        private void UnregisterListeners()
        {
            foreach (IEventListener listener in GetComponents<IEventListener>())
                listener.UnregisterListeners();
        }
        private void AddDestroyedListener()
        {
            if (!GetComponent<DestroyedListener>()) 
                gameObject.AddComponent<DestroyedListener>();
        }

        private void RegisterCollisions()
        {
            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>(includeInactive: true))
            {
                _game.collisionRegistry.Value.Register(collider.GetInstanceID(), this);
                Entity.AddCollider(collider);
            }
        }
        private void UnregisterCollisions()
        {
            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
                _game.collisionRegistry.Value.Unregister(collider.GetInstanceID(), this);
        }

        public void Dispose()
        {
            UnregisterCollisions();
            UnregisterListeners();
        }
    }
}
