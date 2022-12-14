/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Conditions;
using AI.BehaviourTree;
using Node = AI.BehaviourTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.Decorators
{
    public class RepeatNodeView : NodeView
    {
        public Port InputConpitionPort { get; private set; }

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
        public override void Initialize(Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected)
        {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }
        public override void Draw() 
        {
            this.title = "Loop";

            CreateInputPorts();
            CreateOutputPorts();
        }

        public RepeatNodeView(string pathUxml) : base(pathUxml) { }
    }
}