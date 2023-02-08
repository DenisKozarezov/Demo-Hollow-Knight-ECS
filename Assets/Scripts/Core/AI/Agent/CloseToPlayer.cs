using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Core.ECS.Components.Player;
using Core.ECS.Components.Units;

namespace Core.AI.Agent.Conditions
{
    //[Category("Agent/Conditions")]
    //public class CloseToPlayer : Condition
    //{
    //    [SerializeField, Min(0f)]
    //    private float _distance;

    //    private EcsFilter _filter;
    //    private EcsEntity _player;
    //    private Transform _transform;
    //    protected override void OnInit()
    //    {
    //        _transform = Agent.Get<TransformComponent>().Value;
    //        _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>.Exclude<DiedComponent>));
    //    }
    //    protected override void OnStart()
    //    {
    //        _player = _filter.GetEntity(0);
    //    }
    //    protected override bool Check()
    //    {
    //        if (_player.IsNullOrEmpty()) return false;

    //        Vector3 playerPos = _player.Get<TransformComponent>().Value.position;
    //        return (playerPos - _transform.position).sqrMagnitude < _distance * _distance;
    //    }
    //}
}