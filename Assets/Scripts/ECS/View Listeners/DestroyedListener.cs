using System;
using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class DestroyedListener : MonoBehaviour, IEventListener, IDestroyedListener
    {
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDestroyedListener(this);

            OnDestroyed(_entity);
        }
        public void UnregisterListeners() => _entity.RemoveDestroyedListener();
        public void OnDestroyed(GameEntity entity)
        {
            if (entity.isDestroyed)
            {
                foreach (IDisposable disposable in gameObject.GetComponents<IDisposable>())
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
