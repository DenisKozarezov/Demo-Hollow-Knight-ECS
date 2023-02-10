using Entitas;
using UnityEngine;

namespace Core.ECS.ViewListeners
{
    public sealed class GroundedListener : MonoBehaviour, IEventListener, IGroundedListener
    {
        private GameEntity _entity;
        private Animator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddGroundedListener(this);

            _animator = GetComponent<Animator>();

            OnGrounded(_entity);
        }
        public void UnregisterListeners() => _entity.RemoveGroundedListener();
        public void OnGrounded(GameEntity entity)
        {
            if (entity.isGrounded) _animator.SetBool("OnGround", true);
        }
    }
}
