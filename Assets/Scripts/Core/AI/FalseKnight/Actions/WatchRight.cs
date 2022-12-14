using UnityEngine;
using AI.BehaviourTree.Nodes;
using Core.ECS.Components.Units;
using Leopotam.Ecs;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchRight: ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        protected override void OnInit()
        {
            _spriteRenderer = Agent.Get<SpriteRendererComponent>().Value;
        }
        protected override State OnUpdate()
        {
            _spriteRenderer.flipX = false;
            return State.Success;
        }
    }
}