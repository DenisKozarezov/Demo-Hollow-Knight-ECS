using UnityEngine;

namespace Core.ECS.Events
{
    internal struct AnimateDamageEventComponent
    {
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ИЗВНЕ** //
        public GameObject GameObjectRef;
        public Color ColorRef;
        
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ВНУТРИ СИСТЕМЫ DustCloudAnimationSystem** //
        public float TimeAlive;
        public bool Damaged;
    }
}