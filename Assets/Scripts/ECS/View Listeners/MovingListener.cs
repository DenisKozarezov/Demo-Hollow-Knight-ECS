using UnityEngine;
using Entitas;
using Core.Units;

namespace Core.ECS.ViewListeners
{
    public sealed class MovingListener : MonoBehaviour, IEventListener, IMovingListener, IStoppedMovingListener
    {
        private GameEntity _entity;
        private UnitAnimator _animator;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddMovingListener(this);
            _entity.AddStoppedMovingListener(this);

            _animator = GetComponent<UnitAnimator>();
        }
        public void UnregisterListeners()
        {
            _entity.RemoveMovingListener();
            _entity.RemoveStoppedMovingListener();
        }
        public void OnMoving(GameEntity entity) => _animator.PlayRun();
        public void OnStoppedMoving(GameEntity entity) => _animator.PlayIdle();
    }
}
