using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class PositionListener : MonoBehaviour, IEventListener, IPositionListener, IStoppedMovingListener
    {
        private GameEntity _entity;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddPositionListener(this);
            _entity.AddStoppedMovingListener(this);
        }
        public void UnregisterListeners()
        {
            _entity.RemovePositionListener(this);
            _entity.RemoveStoppedMovingListener(this);
        }
        public void OnPosition(GameEntity entity, Vector2 value)
        {
            if (_rigidbody != null)
            {
                Vector2 velocity = (Vector3)value - transform.position;
                _rigidbody.velocity = velocity.SetY(_rigidbody.velocity.y);
            }
            else
                transform.position = value;
        }
        public void OnStoppedMoving(GameEntity entity)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = _rigidbody.velocity.SetX(0f);
            }
        }
    }
}
