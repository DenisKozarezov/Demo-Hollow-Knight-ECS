using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Runtime.Nodes
{
    public abstract class ActionNode : Node
    {
        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }

#if UNITY_EDITOR
        public sealed override IEnumerable<Node> GetChildren()
        {
            return Enumerable.Empty<Node>();
        }
        public sealed override void AddChild(Node node) { }
        public sealed override void RemoveChild(Node node) { }
#endif
    }
}
