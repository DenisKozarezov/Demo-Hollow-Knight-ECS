using UnityEngine;
using BehaviourTree.Runtime.Nodes;

namespace Core.AI.Agent.Actions
{
    [Category("Agent/Actions")]
    public class SetHealth : Action
    {
        [SerializeField, Min(0)]
        private int _value;
        private GameEntity _entity;
        protected override void OnInit()
        {
            _entity = Agent as GameEntity;
        }
        protected override State OnUpdate()
        {
            int newHealth = System.Math.Clamp(_entity.currentHp.Value + _value, 0, _entity.maxHp.Value);
            _entity.ReplaceCurrentHp(newHealth);
            return State.Success;
        }
    }
}