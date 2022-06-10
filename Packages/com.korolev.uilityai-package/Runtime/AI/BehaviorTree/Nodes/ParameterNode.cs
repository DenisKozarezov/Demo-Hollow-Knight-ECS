/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class ParameterNode : Node
    {
        [HideInInspector] public Node ChildNode;
        public override Node Clone()
        {
            ParameterNode other = Instantiate(this);
            other.ChildNode = other.ChildNode?.Clone();
            return other;
        }
    }   
}