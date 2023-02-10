using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class DamageTakenListener : MonoBehaviour, IEventListener, IDamageTakenListener
    {
        private Animator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDamageTakenListener(this);

            _animator = GetComponent<Animator>();
        }
        public void UnregisterListeners() => _entity.RemoveDamageTakenListener();
        public void OnDamageTaken(GameEntity entity)
        {
            _animator.SetTrigger("Hit");
            entity.isDamageTaken = false;
        }
    }
}
