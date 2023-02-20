using UnityEngine;
using Entitas;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class JumpingListener : MonoBehaviour, IEventListener, IJumpingListener
    {
        private GameEntity _entity;
        private UnitAnimator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddJumpingListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners()
        {
            _entity.RemoveMovingListener();
        }
        public void OnJumping(GameEntity entity) => _animator.PlayJump();
    }
}
