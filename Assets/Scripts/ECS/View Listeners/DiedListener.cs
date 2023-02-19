using UnityEngine;
using Entitas;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class DiedListener : MonoBehaviour, IEventListener, IDeadListener
    {
        private GameEntity _entity;
        private UnitAnimator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDeadListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners() => _entity.RemoveDeadListener();
        public void OnDead(GameEntity entity) => _animator.PlayDeath();
    }
}
