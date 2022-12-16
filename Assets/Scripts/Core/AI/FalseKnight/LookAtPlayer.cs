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
        private Transform _agent;
        private EcsFilter _filter;
        private float _startLocalX;

        protected override void OnInit()
        {
            _agent = Agent.Get<SpriteRendererComponent>().Value.transform;
            _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>.Exclude<DiedComponent>));
            _startLocalX = _agent.transform.localScale.x;
        }
        protected override State OnUpdate()
        {
            ref EcsEntity player = ref _filter.GetEntity(0);
            if (player.IsNullOrEmpty()) return State.Failure;

            float playerX = player.Get<SpriteRendererComponent>().Value.transform.position.x;
            float direction = playerX - _agent.transform.position.x;

            Vector3 localScale = _agent.transform.localScale;
            localScale.x = _startLocalX * (direction < 0f ? -1f : 1f);
            _agent.transform.localScale = localScale;
            return State.Success;
        }
    }
}