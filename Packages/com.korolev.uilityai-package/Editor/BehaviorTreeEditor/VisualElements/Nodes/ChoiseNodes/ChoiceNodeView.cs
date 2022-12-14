/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using AI.BehaviourTree;
using AI.BehaviourTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviourTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.Choices
{
    public class ChoiceNodeView: NodeView
    {
        public Port InputParameterPort { get; private set; }

        protected override void CreateInputPorts() 
        {
            base.CreateInputPorts();
            
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                InputParameterPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(ParameterNode));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                InputParameterPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(ParameterNode));
            
            InputParameterPort.name = "";
            InputParameterPort.portName = "Parameter";
            inputContainer.Add(InputParameterPort);
        }
        public override void Initialize(Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected)
        {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }      
        public override void Draw() 
        {
            this.title = "Choice";
            
            CreateInputPorts();           
            CreateOutputPorts();            
            RefreshPorts();
            RefreshExpandedState();
        }        

        public ChoiceNodeView(string pathUxml) : base(pathUxml) { }
    }
}