using System.Linq;
using UnityEngine;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Core.Units;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class DistanceToPlayer : FloatNode
    {
        private Transform _player;
        private Transform _transform;

        protected override void OnInit()
        {
            _transform = BehaviorTreeRef.EntityReference.transform;
            _player = FindObjectsOfType<UnitScript>().Where(i => i.gameObject.layer == Constants.PlayerLayer).First().transform;
        }
        protected override State OnUpdate() 
        {               
            if (_player != null && _transform != null)
            {
                Value = (_transform.position - _player.position).magnitude;
            }
            return State.Success; 
        }
    }
}