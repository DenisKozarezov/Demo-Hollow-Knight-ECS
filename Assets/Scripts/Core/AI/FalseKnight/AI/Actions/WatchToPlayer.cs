using System;
using System.Linq;
using AI.BehaviorTree.Nodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchToPlayer : ActionNode
    {
        [NonSerialized] private GameObject PlayerRef;
        [NonSerialized] private GameObject GameObjectRef;

        public override void OnInit()
        {
            base.OnInit();
            PlayerRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.PlayerLayer).FirstOrDefault();
            GameObjectRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.EnemyLayer).FirstOrDefault();
        }

        public override State OnUpdate()
        {
            var directionWatch = (PlayerRef.transform.position - GameObjectRef.transform.position).x;
            if (directionWatch > 0)
                GameObjectRef.transform.localScale = new Vector3(1, GameObjectRef.transform.localScale.y, GameObjectRef.transform.localScale.z);
            
            if( directionWatch < -0.1f)
                GameObjectRef.transform.localScale = new Vector3(-1, GameObjectRef.transform.localScale.y, GameObjectRef.transform.localScale.z);

            return State.Success;
        }
    }
}