/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public abstract class CompositeNode : Node
    {
        public AnimationCurve Curve; 
        [HideInInspector] public List<Node> ChildNodes = new List<Node>();

        protected override void OnInit() { }
        protected override void OnStart() { }
        protected override void OnStop() { }
        public override IEnumerable<Node> GetChildren()
        {
            return ChildNodes;
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
