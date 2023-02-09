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
        public void UnregisterListeners(IEntity with) => _entity.RemoveDestroyedListener();
        public void OnDestroyed(GameEntity entity)
        {
            if (entity.isDestroyed)
            {
                IViewController controller = gameObject.GetComponent<IViewController>();
                controller.Destroy();
            }
        }
    }
}
