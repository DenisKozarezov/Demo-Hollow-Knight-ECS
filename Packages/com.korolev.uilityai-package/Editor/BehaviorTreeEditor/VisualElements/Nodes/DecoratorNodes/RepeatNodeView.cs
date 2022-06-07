/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using AI.BehaviorTree;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.ConditionNodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviorTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.DecoratorNodes
{
    public class RepeatNodeView : NodeView
    {
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = "Loop";
            CreateInputPorts();
            CreateOutputPorts();
        }

        protected override void CreateInputPorts()
        {
            base.CreateInputPorts();
            
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                InputConpitionPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(ConditionNodeView));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                InputConpitionPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(ConditionNodeView));
            
            InputConpitionPort.name = "";
            InputConpitionPort.portName = "Condition";
            inputContainer.Add(InputConpitionPort);
        }

        protected override void CreateOutputPorts()
        {
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            
            OutputPort.name = "";
            OutputPort.portName = "";
            outputContainer.Add(OutputPort);
        }

        public Port InputConpitionPort { get; set; }

        public RepeatNodeView(string pathUxml) : base(pathUxml) { }
    }
}