/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class ParameterNode : Node
    {
        [HideInInspector] public Node ChildNode;

        public override IEnumerable<Node> GetChildren()
        {
            yield return ChildNode;
        }
        public override Node Clone()
        {
            ParameterNode clone = Instantiate(this);
            clone.ChildNode = null;
            return clone;
        }
    }   
}