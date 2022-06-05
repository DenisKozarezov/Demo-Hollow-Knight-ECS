using System;
using UnityEngine;

namespace AI.BehaviorTree.Nodes.ParameterNodes
{
    public class BooleanNode: ParameterNode
    {
        [NonSerialized] public bool Value;

        public override void OnInit() {Value = false; }
        public override void OnStart() { State = State.Running; }
        public override void OnStop() { }
        public override State OnUpdate() {
            return State.Success;
        }
    }
}