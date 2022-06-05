using System;
using UnityEngine;

namespace Examples.Example_1.ECS.Events.FalseKnight
{
    [Serializable]
    public struct FalseKnightAttackEventComponent
    {
        //** ПААМЕТРЫ ДЛЯ ИЗМЕНЕНИЯ ИЗВНЕ** //
        public GameObject GameObjectRef;
    }
}