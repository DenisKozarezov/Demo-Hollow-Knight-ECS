namespace Core.ECS.Events
{
    public struct CameraShakeEventComponent
    {
        public float ShakeDuration;
        public float ShakeForce;
    }
    public struct CameraFadeEventComponent 
    { 
        public float FadeTime; 
        public FadeMode FadeMode;
    }
}