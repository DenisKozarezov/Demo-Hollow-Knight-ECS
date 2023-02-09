using UnityEngine;

namespace Core.ECS.ViewComponentRegistrators
{
    public sealed class RigidbodyComponentRegistrator : MonoBehaviour, IViewComponentRegistrator
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void Register(GameEntity entity) => entity.AddRigidbody(_rigidbody);
    }
}
