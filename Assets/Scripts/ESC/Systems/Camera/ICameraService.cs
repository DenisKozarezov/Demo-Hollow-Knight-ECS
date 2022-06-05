using UnityEngine;

namespace Core.Services
{
    public interface ICameraService
    {
        float Speed { get; }
        Transform Target { get; }
        
        void Attach(Transform target);
        void Detach();
    }
}