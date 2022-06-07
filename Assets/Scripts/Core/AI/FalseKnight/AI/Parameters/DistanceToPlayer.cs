using System;
using System.Linq;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class DistanceToPlayer : FloatNode
    {
        [NonSerialized] private GameObject PlayerRef;
        [NonSerialized] private GameObject GameObjectRef;

        public override void OnStart() 
        { 
            PlayerRef = FindObjectsOfType<GameObject>().Where(i => i.layer == LayerMask.NameToLayer("Character")).FirstOrDefault();
            GameObjectRef = FindObjectsOfType<GameObject>().Where(i => i.layer == LayerMask.NameToLayer("FalseKnight")).FirstOrDefault();
        }
        public override State OnUpdate() 
        {   
            
            if (PlayerRef != null && GameObjectRef != null)
            {
                var distance = Mathf.Abs((GameObjectRef.transform.position - PlayerRef.transform.position).magnitude);
                this.Value = distance;
            }
            return State.Success; 
        }
    }
}