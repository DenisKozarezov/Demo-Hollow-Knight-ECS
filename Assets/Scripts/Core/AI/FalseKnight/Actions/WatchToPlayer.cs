using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using Core.Units;
using AI.BehaviorTree.Nodes;
using Examples.Example_1.ECS;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class WatchToPlayer : ActionNode
    {
        private Transform _player;
        private SpriteRenderer _spriteRenderer;

        protected override void OnInit()
        {
            _spriteRenderer = BehaviorTreeRef.EntityReference.Entity.Get<SpriteRendererComponent>().Value;
            _player = FindObjectsOfType<UnitScript>().Where(i => i.gameObject.layer == Constants.PlayerLayer).First().transform;
        }
        protected override State OnUpdate()
        {
            float directionWatch = (_player.position - _spriteRenderer.transform.position).x;
            _spriteRenderer.flipX = directionWatch < -0.1f;
            return State.Success;
        }
    }
}