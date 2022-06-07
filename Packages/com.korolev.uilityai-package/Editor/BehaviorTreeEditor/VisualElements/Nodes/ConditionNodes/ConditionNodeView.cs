using System;
using AI.BehaviorTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.ConditionNodes
{
    public class ConditionNodeView : NodeView
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
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ConditionNodeView));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(ConditionNodeView));
            
            OutputPort.name = "";
            OutputPort.portName = "";
            outputContainer.Add(OutputPort);
        }
        
        public ConditionNodeView(string pathUxml) : base(pathUxml) { }
    }
}