/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using UnityEngine;

namespace AI.BehaviorTree.Nodes.ParameterNodes
{
    public class FloatNode : ParameterNode
    { 
        public float Value;

        public override void OnInit() {Value = 0;}

        public override void OnStart() { State = State.Running; }
        public override void OnStop() { }
        public override State OnUpdate() {
            return State.Success;
        }
    }
}