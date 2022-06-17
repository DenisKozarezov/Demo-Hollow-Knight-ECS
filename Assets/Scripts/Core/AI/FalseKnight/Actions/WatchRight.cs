using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using AI.BehaviorTree.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchRight: ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
        }
        protected override State OnUpdate()
        {
            _spriteRenderer.flipX = false;
            return State.Success;
        }
    }
}