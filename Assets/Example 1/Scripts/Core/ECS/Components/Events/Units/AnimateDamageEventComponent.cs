using System;
using UnityEngine;

namespace Examples.Example_1.ECS.Events
{
    [Serializable]
    public struct AnimateDamageEventComponent
    {
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ИЗВНЕ** //
        public GameObject GameObjectRef;
        public Color ColorRef;
        
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ВНУТРИ СИСТЕМЫ DustCloudAnimationSystem** //
        public float TimeAlive;
        public bool Damaged;
    }
}