using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.AI.FalseKnight.Actions
{
    public class LookAtPlayer : Action
    {
        private SpriteRenderer _spriteRenderer;
        private EcsFilter _filter;
        private EcsEntity _player;

        protected override void OnInit()
        {
            _spriteRenderer = Agent.Get<SpriteRendererComponent>().Value;
            _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>.Exclude<DiedComponent>));
        }
        protected override void OnStart()
        {
            _player = _filter.GetEntity(0);
        }
        protected override State OnUpdate()
        {
            if (_player.IsNullOrEmpty()) return State.Failure;

            Transform transform = _player.Get<SpriteRendererComponent>().Value.transform;
            float directionWatch = (transform.position - _spriteRenderer.transform.position).x;
            bool isLeft = directionWatch < -0.1f ? true : false;
            _spriteRenderer.flipX = isLeft;
            return State.Success;
        }
    }
}