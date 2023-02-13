using UnityEngine;
using Entitas;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class DiedListener : MonoBehaviour, IEventListener, IDiedListener
    {
        private GameEntity _entity;
        private UnitAnimator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDiedListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners() => _entity.RemoveDiedListener();
        public void OnDied(GameEntity entity) => _animator.PlayDeath();
    }
}
