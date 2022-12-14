/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree.Nodes
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node Child;

        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public override IEnumerable<Node> GetChildren()
        {
            yield return Child;
        }
        public override void RemoveChild(Node child)
        {
            if (!child.Equals(Child)) return;

            Child.Parent = null;
            Child = null;
        }
        public override Node Clone()
        {
            DecoratorNode clone = Instantiate(this);
            clone.GUID = GUID;
            clone.Parent = null;
            clone.Child = null;
            return clone;
        }
    }   
}
