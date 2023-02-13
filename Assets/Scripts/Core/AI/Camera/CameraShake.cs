using UnityEngine;
using BehaviourTree.Runtime.Nodes;

namespace Core.AI.Camera
{
    [Category("Camera")]
    public class CameraShake : Action
    {
        [SerializeField, Min(0f)]
        private float _duration;
        [SerializeField, Min(0f)]
        private float _shakeForce;

        protected override State OnUpdate()
        {
            ECSExtensions.Empty().AddCameraShake(newShakeDuration: _duration, newShakeForce: _shakeForce);
            return State.Success;
        }
    }
}