using System;
using System.Linq;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Parameters
{
    public class Grounded : BooleanNode
    {
        [NonSerialized] private GameObject GameObjectRef;
        [NonSerialized] private GameObject GroundGameObjectRef;
        public float Distance;

        public override void OnInit()
        {
            GameObjectRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.EnemyLayer).FirstOrDefault();
            GroundGameObjectRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.GroundLayer).FirstOrDefault();
        }

        public override void OnStart() { }
        public override void OnStop() { }

        public override State OnUpdate()
        {
            Distance = Mathf.Abs(GroundGameObjectRef.transform.position.y - GameObjectRef.transform.position.y);
            if (Distance < 0.8f)
                Value = true;
            else
                Value = false;
            return State.Success;
        }
    }
}