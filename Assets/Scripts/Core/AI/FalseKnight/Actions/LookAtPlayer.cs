using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
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
            float isLeft = directionWatch < -0.1f ? -1f : 1f;

            Vector3 localScale = _spriteRenderer.transform.localScale;
            localScale.x *= isLeft;
            _spriteRenderer.transform.localScale = localScale;
            return State.Success;
        }
    }
}