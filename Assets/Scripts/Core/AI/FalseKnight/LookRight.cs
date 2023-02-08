using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    //[Category("False Knight/Actions")]
    //public class LookRight : Action
    //{
    //    private Transform _agent;
    //    private float _startLocalX;
    //    protected override void OnInit()
    //    {
    //        _agent = Agent.Get<TransformComponent>().Value;
    //        _startLocalX = _agent.localScale.x;
    //    }
    //    protected override State OnUpdate()
    //    {
    //        Vector3 localScale = _agent.localScale;
    //        localScale.x = _startLocalX;
    //        _agent.localScale = localScale;
    //        return State.Success;
    //    }
    //}
}