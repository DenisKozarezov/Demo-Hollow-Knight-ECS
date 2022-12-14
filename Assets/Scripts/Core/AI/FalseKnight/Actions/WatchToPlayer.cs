using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using AI.BehaviorTree.Nodes;
using Core.Units;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchToPlayer : ActionNode
    {
        private Transform _player;
        private SpriteRenderer _spriteRenderer;

        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.Agent.Get<SpriteRendererComponent>().Value;
            _player = FindObjectsOfType<UnitView>().Where(i => i.gameObject.layer == Constants.PlayerLayer).First().transform;
        }
        protected override State OnUpdate()
        {
            float directionWatch = (_player.position - _spriteRenderer.transform.position).x;
            bool isLeft = directionWatch < -0.1f ? true : false;
            _spriteRenderer.flipX = isLeft;
            return State.Success;
        }
    }
}