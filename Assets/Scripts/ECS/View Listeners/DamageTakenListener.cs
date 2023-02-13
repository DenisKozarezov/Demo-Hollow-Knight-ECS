using UnityEngine;
using Entitas;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class DamageTakenListener : MonoBehaviour, IEventListener, IDamageTakenListener
    {
        private UnitAnimator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDamageTakenListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners() => _entity.RemoveDamageTakenListener();
        public void OnDamageTaken(GameEntity entity, int value) => _animator.PlayDamageTaken();
    }
}
