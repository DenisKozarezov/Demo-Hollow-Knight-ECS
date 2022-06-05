/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using Leopotam.Ecs;
using UnityEngine;

namespace AI.ECS.Components
{
    [Serializable]
    public struct BehaviorTreeComponent
    {
        [SerializeField] public GameObject GameObjectRef;
        [SerializeField] public BehaviorTree.BehaviorTree BehaviorTree;
        [SerializeField] public bool isInitialised ;
        
        public void Init(EcsWorld ecsWorld) { BehaviorTree.Init(ecsWorld);}

        public void ValidateRefferences() { BehaviorTree.GameObjectRef = GameObjectRef; }
    }
}