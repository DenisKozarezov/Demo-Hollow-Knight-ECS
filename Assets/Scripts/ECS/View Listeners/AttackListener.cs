using UnityEngine;
using Entitas;
using Core.ECS.Components.Units;

namespace Core.ECS.ViewListeners
{
    public class AttackListener : MonoBehaviour, IEventListener, IAttackingListener
    {
        private Animator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddAttackingListener(this);

            _animator = GetComponent<Animator>();
        }
        public void UnregisterListeners() => _entity.RemoveAttackingListener();
        public void OnAttacking(GameEntity entity, AttackDirection direction)
        {
            switch (direction)
            {
                case AttackDirection.Default:
                    _animator.SetTrigger("Attack");
                    break;
                case AttackDirection.Up:
                    _animator.SetTrigger("AttackUp");
                    break;
                case AttackDirection.Down:
                    _animator.SetTrigger("AttackDown");
                    break;
            }        
        }
    }
}