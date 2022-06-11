using System.Linq;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class DistanceToPlayer : FloatNode
    {
        private GameObject PlayerRef;
        private Transform _transform;

        protected override void OnStart()
        {
            _transform = BehaviorTreeRef.EntityReference.transform;
            PlayerRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.PlayerLayer).FirstOrDefault();
        }
        protected override State OnUpdate() 
        {               
            if (PlayerRef != null && _transform != null)
            {
                Value = (_transform.position - PlayerRef.transform.position).magnitude;
            }
            return State.Success; 
        }
    }
}