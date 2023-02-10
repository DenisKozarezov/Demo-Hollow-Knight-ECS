using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class PositionListener : MonoBehaviour, IPositionListener, IEventListener
    {
        private GameEntity _entity;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void OnPosition(GameEntity entity, Vector2 value)
        {
            if (_rigidbody != null)
                _rigidbody.position = value;
            else
                transform.position = value;
        }
        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddPositionListener(this);
        }
        public void UnregisterListeners() => _entity.RemovePositionListener(this);
    }
}
