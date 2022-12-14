using System;
using AI.BehaviourTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviourTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.Conditions
{
    public class ConditionNodeView : NodeView
    {
        protected override void CreateOutputPorts()
        {
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ConditionNodeView));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(ConditionNodeView));
            
            OutputPort.name = "";
            OutputPort.portName = "";
            outputContainer.Add(OutputPort);
        }
        public override void Initialize(Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) 
        {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }
        public override void Draw() 
        {
            this.title = Node.name;
            CreateOutputPorts();
        }
        
        public ConditionNodeView(string pathUxml) : base(pathUxml) { }
    }
}