using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class MovingListener : MonoBehaviour, IEventListener, IMovingListener, IStoppedMovingListener
    {
        private Animator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddMovingListener(this);
            _entity.AddStoppedMovingListener(this);

            _animator = GetComponent<Animator>();
        }
        public void UnregisterListeners()
        {
            _entity.RemoveMovingListener();
            _entity.RemoveStoppedMovingListener();
        }
        public void OnMoving(GameEntity entity) => _animator.SetBool("IsMoving", true);
        public void OnStoppedMoving(GameEntity entity) => _animator.SetBool("IsMoving", false);
    }
}
