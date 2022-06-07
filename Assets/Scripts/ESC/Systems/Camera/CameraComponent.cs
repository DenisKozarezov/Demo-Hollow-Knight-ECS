using UnityEngine;

namespace Core.Services
{
    [DisallowMultipleComponent]
    public class CameraComponent : MonoBehaviour, ICameraService
    {
        [SerializeField]
        private float _speed;

        public float Speed => _speed;
        public Transform Target { get; private set; }

        public void Attach(Transform target)
        {
            Target = target;
        }
        public void Detach()
        {
            Target = null;
        }
    }
}