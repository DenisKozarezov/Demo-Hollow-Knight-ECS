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

            if (directionWatch < -0.1f)
                _gameObject.transform.localScale = new Vector3((_gameObject.transform.localScale.x < 0) ? _gameObject.transform.localScale.x : _gameObject.transform.localScale.x * -1, _gameObject.transform.localScale.y, _gameObject.transform.localScale.z);
            else
                _gameObject.transform.localScale = new Vector3((_gameObject.transform.localScale.x > 0) ? _gameObject.transform.localScale.x : _gameObject.transform.localScale.x * -1, _gameObject.transform.localScale.y, _gameObject.transform.localScale.z);

            return State.Success;
        }
    }
}