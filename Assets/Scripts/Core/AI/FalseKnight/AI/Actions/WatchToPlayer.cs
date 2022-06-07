using System;
using System.Linq;
using AI.BehaviorTree.Nodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchToPlayer : ActionNode
    {
        [NonSerialized] private GameObject PlayerRef;
        private SpriteRenderer _spriteRenderer;

        public override void OnStart()
        {
            _spriteRenderer = BehaviorTreeRef.GameObjectRef.GetComponent<SpriteRenderer>();
            PlayerRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.PlayerLayer).FirstOrDefault();
        }
        public override void OnStop() { }
        public override State OnUpdate()
        {
            float directionWatch = (PlayerRef.transform.position - _spriteRenderer.transform.position).x;
            _spriteRenderer.flipX = directionWatch < -0.1f;
            return State.Success;
        }
    }
}