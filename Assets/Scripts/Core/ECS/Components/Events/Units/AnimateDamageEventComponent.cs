using UnityEngine;

namespace Core.ECS.Events
{
    internal struct AnimateDamageEventComponent
    {
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ИЗВНЕ** //
        public GameObject GameObjectRef;
        
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ВНУТРИ СИСТЕМЫ DustCloudAnimationSystem** //
        public float Duration;
        public bool Damaged;
    }
}