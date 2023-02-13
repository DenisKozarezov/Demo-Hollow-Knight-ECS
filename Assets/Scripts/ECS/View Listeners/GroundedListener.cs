using Entitas;
using UnityEngine;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class GroundedListener : MonoBehaviour, IEventListener, IGroundedListener
    {
        private GameEntity _entity;
        private UnitAnimator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddGroundedListener(this);

            _animator = GetComponent<UnitAnimator>();

            OnGrounded(_entity);
        }
        public void UnregisterListeners() => _entity.RemoveGroundedListener();
        public void OnGrounded(GameEntity entity)
        {
            _animator.Do(x => x.PlayGrounded(), when: entity.isGrounded);
        }
    }
}
