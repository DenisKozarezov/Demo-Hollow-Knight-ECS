using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.AI.FalseKnight.Actions
{
    //[Category("False Knight/Actions")]
    //public class LookAtPlayer : Action
    //{
    //    private Transform _agent;
    //    private EcsFilter _filter;
    //    private float _startLocalX;

    //    protected override void OnInit()
    //    {
    //        _agent = Agent.Get<TransformComponent>().Value;
    //        _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>.Exclude<DiedComponent>));
    //        _startLocalX = _agent.localScale.x;
    //    }
    //    protected override State OnUpdate()
    //    {
    //        ref EcsEntity player = ref _filter.GetEntity(0);
    //        if (player.IsNullOrEmpty()) return State.Failure;

    //        float playerX = player.Get<TransformComponent>().Value.position.x;
    //        float direction = playerX - _agent.position.x;

    //        Vector3 localScale = _agent.localScale;
    //        localScale.x = _startLocalX * (direction < 0f ? -1f : 1f);
    //        _agent.localScale = localScale;
    //        return State.Success;
    //    }
    //}
}