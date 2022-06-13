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
        public EntityReference EntityReference;
        public BehaviorTree.BehaviorTree BehaviorTree;
    
        public void Init(EcsWorld ecsWorld) 
        {
            BehaviorTree = BehaviorTree.Clone();
            BehaviorTree.Init(ecsWorld);
            BehaviorTree.EntityReference = EntityReference;         
        }
    }
}