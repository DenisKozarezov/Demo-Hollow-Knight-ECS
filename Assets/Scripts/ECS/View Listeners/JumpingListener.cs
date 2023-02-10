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

            _animator = GetComponent<Animator>();
        }
        public void UnregisterListeners()
        {
            _entity.RemoveMovingListener();
            _entity.RemoveStoppedMovingListener();
        }
        public void OnJumping(GameEntity entity)
        {
            if (entity.isJumping)
            {
                _animator.SetTrigger("Jump");
                _animator.SetBool("IsJumping", true);
                _animator.SetBool("OnGround", false);
            }
        }
    }
}
