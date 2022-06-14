/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class ActionNode : Node
    {
        public AnimationCurve Curve;

        public override IEnumerable<Node> GetChildren()
        {
            return null;
        }
        public override Node Clone()
        {
            ActionNode clone = Instantiate(this);
            clone.Parent = null;
            clone.GUID = GUID;
            return clone;
        }
    }
}
