using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;
using Leopotam.Ecs;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchLeft: ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
        }
        protected override State OnUpdate()
        {
            _spriteRenderer.flipX = true;
            return State.Success;
        }
    }
}