using System;
using AI.BehaviorTree;
using AI.BehaviorTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.ParameterNodes
{
    public class BooleanNodeView: NodeView
    {
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = Node.name;
            CreateOutputPorts();
        }

        protected virtual void CreateOutputPorts()
        {
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ParameterNode));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(ParameterNode));
            
            OutputPort.name = "";
            OutputPort.portName = "Parameter";
            outputContainer.Add(OutputPort);
        }
        
        public BooleanNodeView(string pathUxml) : base(pathUxml) { }
    }
}