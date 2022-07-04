using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using Core.Units;
using Core.ECS.Components.Units;
using AI.BehaviorTree.Nodes;

namespace Core.AI.FalseKnight.Actions
{
    public class WatchToPlayer : ActionNode
    {
        private Transform _player;
        private GameObject _gameObject;

        protected override void OnInit()
        {
            _gameObject = BehaviorTreeRef.EntityReference.gameObject;
            _player = FindObjectsOfType<UnitScript>().Where(i => i.gameObject.layer == Constants.PlayerLayer).First().transform;
        }
        protected override State OnUpdate()
        {
            float directionWatch = (_player.position - _gameObject.transform.position).x;
           
            Vector3 scale = _gameObject.transform.localScale;
            scale.x = directionWatch < -0.1f ? -1f : 1f;
            _gameObject.transform.localScale = scale;
            return State.Success;
        }
    }
}