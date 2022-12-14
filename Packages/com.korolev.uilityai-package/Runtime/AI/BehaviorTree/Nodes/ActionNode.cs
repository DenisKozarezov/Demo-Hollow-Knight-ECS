/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System.Collections.Generic;

namespace AI.BehaviourTree.Nodes
{
    public abstract class ActionNode : Node
    {
        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public override IEnumerable<Node> GetChildren()
        {
            return null;
        }
        public override void RemoveChild(Node child)
        {
            return;
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
