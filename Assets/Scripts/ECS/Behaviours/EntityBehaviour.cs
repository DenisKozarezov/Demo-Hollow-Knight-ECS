﻿using System;
using UnityEngine;
using Entitas.VisualDebugging.Unity;
using Core.ECS.ViewListeners;

namespace Core.ECS.Behaviours
{
    public abstract class EntityBehaviour : MonoBehaviour, IDisposable
    {
        protected GameEntity Entity { get; private set; }
        protected GameContext Game => Contexts.sharedInstance.game;

        protected virtual void Awake()
        {
            Entity = Game.CreateEntity();

            gameObject.AddComponent<ViewController>().InitializeView(Game, Entity);

            foreach (var listener in GetComponents<IEventListener>())
            {
                if (listener.GetType() == this.GetType()) continue;

                listener.RegisterListeners(Entity);
            }
        }
        public virtual void Dispose() => gameObject.DestroyGameObject();
    }
}