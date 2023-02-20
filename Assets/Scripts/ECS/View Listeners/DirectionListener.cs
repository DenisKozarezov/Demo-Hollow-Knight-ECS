using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class DirectionListener : MonoBehaviour, IEventListener, IDirectionListener
    {
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDirectionListener(this);

            UpdateCurrentDirection();
        }
        public void UnregisterListeners() => _entity.RemoveDirectionListener();
        public void OnDirection(GameEntity entity, float direction)
        {
            float currentXScaleValue = Mathf.Abs(transform.localScale.x);
            transform.localScale = transform.localScale.SetX(currentXScaleValue * direction);
        }
        private void UpdateCurrentDirection()
        {
            if (_entity.hasDirection) OnDirection(_entity, _entity.direction.Value);
        }
    }
}
