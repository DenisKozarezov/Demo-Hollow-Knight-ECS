using System;
using AI.BehaviourTree;
using AI.BehaviourTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviourTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.Parameters
{
    public class FloatNodeView: NodeView
    {
        protected override void CreateOutputPorts()
        {
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ParameterNode));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(ParameterNode));
            
            OutputPort.name = "";
            OutputPort.portName = "Parameter";
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
        
        public FloatNodeView(string pathUxml) : base(pathUxml) { }
    }
}