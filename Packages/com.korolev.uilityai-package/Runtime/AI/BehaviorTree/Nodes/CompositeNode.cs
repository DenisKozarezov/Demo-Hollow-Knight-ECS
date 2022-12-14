using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree.Nodes
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> ChildNodes = new List<Node>();

        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public override IEnumerable<Node> GetChildren()
        {
            return ChildNodes;
        }
        public override void RemoveChild(Node child)
        {
            child.Parent = null;
            ChildNodes.Remove(child);
        }
        public override Node Clone()
        {
            CompositeNode clone = Instantiate(this);
            clone.ChildNodes = new List<Node>();
            clone.Parent = null;
            clone.GUID = GUID;
            return clone;
        }
    }  
}
