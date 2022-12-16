using UnityEngine;
using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using Core.ECS.Components.Units;

namespace Core.AI.FalseKnight.Actions
{
    [Category("False Knight/Actions")]
    public class LookLeft : Action
    {
        private Transform _agent;
        private float _startLocalX;
        protected override void OnInit()
        {
            _agent = Agent.Get<SpriteRendererComponent>().Value.transform;
            _startLocalX = _agent.transform.localScale.x;
        }
        protected override State OnUpdate()
        {
            Vector3 localScale = _agent.transform.localScale;
            localScale.x = -_startLocalX;
            _agent.transform.localScale = localScale;
            return State.Success;
        }
    }
}