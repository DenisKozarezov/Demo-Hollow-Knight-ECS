using System.Linq;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchToPlayer : ActionNode
    {
        private GameObject PlayerRef;
        private SpriteRenderer _spriteRenderer;

        protected override void OnStart()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
            PlayerRef = FindObjectsOfType<GameObject>().Where(i => i.layer == Constants.PlayerLayer).FirstOrDefault();
        }
        protected override void OnStop() { }
        protected override State OnUpdate()
        {
            float directionWatch = (PlayerRef.transform.position - _spriteRenderer.transform.position).x;
            _spriteRenderer.flipX = directionWatch < -0.1f;
            return State.Success;
        }
    }
}