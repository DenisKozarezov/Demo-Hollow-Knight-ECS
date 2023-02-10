using UnityEngine;
using Entitas;

namespace Core.ECS.ViewListeners
{
    public sealed class DiedListener : MonoBehaviour, IEventListener, IDiedListener
    {
        private Animator _animator;
        private GameEntity _entity;

        public void RegisterListeners(IEntity entity)
        {
            _entity = (GameEntity)entity;
            _entity.AddDiedListener(this);

            _animator = GetComponent<Animator>();
        }
        public void UnregisterListeners() => _entity.RemoveDiedListener();
        public void OnDied(GameEntity entity) => _animator.SetTrigger("Death");
    }
}
