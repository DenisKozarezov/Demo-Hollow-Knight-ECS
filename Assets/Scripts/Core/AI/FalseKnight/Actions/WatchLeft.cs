using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using AI.BehaviorTree.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchLeft: ActionNode
    {
        private GameObject _gameObject;
        protected override void OnInit()
        {
            _gameObject = BehaviorTreeRef.EntityReference.gameObject;
        }
        protected override State OnUpdate()
        {
            Vector3 scale = _gameObject.transform.localScale;
            scale.x = 1f;
            _gameObject.transform.localScale = scale;
            return State.Success;
        }
    }
}