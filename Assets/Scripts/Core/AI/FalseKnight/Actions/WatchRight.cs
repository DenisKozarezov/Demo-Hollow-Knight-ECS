using UnityEngine;
using Leopotam.Ecs;
using Core.ECS.Components.Units;
using AI.BehaviorTree.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchRight: ActionNode
    {
        private GameObject _gameObject;
        protected override void OnInit()
        {
            _gameObject = BehaviorTreeRef.EntityReference.gameObject;
        }
        protected override State OnUpdate()
        {
            _gameObject.transform.localScale = new Vector3((_gameObject.transform.localScale.x > 0) ? _gameObject.transform.localScale.x : _gameObject.transform.localScale.x * -1, _gameObject.transform.localScale.y, _gameObject.transform.localScale.z);
            return State.Success;
        }
    }
}