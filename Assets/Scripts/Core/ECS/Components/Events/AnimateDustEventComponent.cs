using UnityEngine;

namespace Core.ECS.Events
{
    public struct AnimateDustEventComponent
    {
        public Vector3 Point;
        public Vector3 Scale;
        public GameObject DustAnimation;
        public float TimeAlive;
    }
}