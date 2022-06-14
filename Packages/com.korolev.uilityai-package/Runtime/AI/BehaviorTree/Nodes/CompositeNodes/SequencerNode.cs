/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;

namespace AI.BehaviorTree.Nodes.CompositeNodes
{
    public class SequencerNode : CompositeNode
    {
        [NonSerialized] private int _currentNodeIndex = 0;

        public SequencerNode() { }
        public SequencerNode(List<Node> childNodes) { this.ChildNodes = childNodes; }

        protected override void OnStart() 
        { 
            _currentNodeIndex = 0;
        }
        protected override State OnUpdate() 
        {
            if (ChildNodes.Count > 0)
            {
                if (_currentNodeIndex >= ChildNodes.Count)
                    return State.Success;
                
                if (ChildNodes[_currentNodeIndex].State == State.Failure)
                    return State.Failure;

                BehaviorTreeRef.SetCurrentNode(ChildNodes[_currentNodeIndex]);
                _currentNodeIndex++;
                return State.Running;
            }
            return State.Success;
        }
        
        public override Node Clone()
        {
            SequencerNode clone = Instantiate(this);
            clone.Parent = null;
            clone.ChildNodes = new List<Node>();
            clone.GUID = GUID;
            return clone;
        }
    }
}