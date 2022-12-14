using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviourTree.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchLeft: ActionNode
    {
        private SpriteRenderer _spriteRenderer;
        protected override void OnInit()
        {
            _spriteRenderer = Agent.Get<SpriteRendererComponent>().Value;
        }
        protected override State OnUpdate()
        {
            _spriteRenderer.flipX = true;
            return State.Success;
        }
    }
}