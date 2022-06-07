using System;
using System.Linq;
using AI.BehaviorTree.Nodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchLeft: ActionNode
    {
        [NonSerialized] private GameObject GameObjectRef;

        public override void OnStart()
        {
            GameObjectRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.EnemyLayer).FirstOrDefault();
        }
        public override void OnStop()
        {
  
        }
        public override State OnUpdate()
        {
            GameObjectRef.transform.localScale = new Vector3(-1, GameObjectRef.transform.localScale.y, GameObjectRef.transform.localScale.z);
            return State.Success;
        }
    }
}