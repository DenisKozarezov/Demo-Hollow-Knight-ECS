using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class JumpingListener : MonoBehaviour, IEventListener, IJumpingListener
    {
        private Animator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddJumpingListener(this);
        }
        public void UnregisterListeners(IEntity with)
        {
            _entity.RemoveMovingListener();
            _entity.RemoveStoppedMovingListener();
        }
        public void OnJumping(GameEntity entity) => _animator.SetTrigger("Jump");
    }
}
