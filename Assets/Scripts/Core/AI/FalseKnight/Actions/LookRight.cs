using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class LookRight : Action
    {
        private SpriteRenderer _spriteRenderer;
        protected override void OnInit()
        {
            _spriteRenderer = Agent.Get<SpriteRendererComponent>().Value;
        }
        protected override State OnUpdate()
        {
            Vector3 localScale = _spriteRenderer.transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            _spriteRenderer.transform.localScale = localScale;
            return State.Success;
        }
    }
}