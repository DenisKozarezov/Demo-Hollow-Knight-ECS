using System;
using UnityEngine;
using Entitas.VisualDebugging.Unity;
using Core.ECS.ViewListeners;

namespace Core.ECS.Behaviours
{
    public abstract class EntityBehaviour : MonoBehaviour, IDisposable
    {
        private IViewController _controller;

        protected GameContext Game => Contexts.sharedInstance.game;
        public GameEntity Entity => _controller.Entity;

        protected virtual void Awake() => _controller = gameObject.GetComponent<IViewController>();

        public virtual void Dispose() => gameObject.DestroyGameObject();
    }
}
