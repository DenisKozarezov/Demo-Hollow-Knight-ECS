using UnityEngine;
using Entitas;
using Core.Units;
using Core.ECS.Components.Units;

namespace Core.ECS.ViewListeners
{
    public class AttackListener : MonoBehaviour, IEventListener, IAttackingListener
    {
        private UnitAnimator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddAttackingListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners() => _entity.RemoveAttackingListener();
        public void OnAttacking(GameEntity entity, AttackDirection direction)
        {
            switch (direction)
            {
                case AttackDirection.Down:
                    _animator.PlayAttack(entity.isGrounded ? AttackDirection.Default : direction);
                    break;
                default: _animator.PlayAttack(direction); 
                    break;
            }
        }
    }
}