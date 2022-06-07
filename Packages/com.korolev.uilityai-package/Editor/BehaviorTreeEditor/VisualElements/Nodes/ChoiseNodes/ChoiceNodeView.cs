/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using AI.BehaviorTree;
using AI.BehaviorTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviorTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.ChoiseNodes
{
    public class ChoiceNodeView: NodeView
    {
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public Port InputParameterPort;
        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = "Choice";
            
            /*INPUT CONTAINER*/
            CreateInputPorts();
            
            /*OUTPUT CONTAINER*/
            CreateOutputPorts();

            /*EXTENSION CONTAINER*/
            
            RefreshPorts();
            RefreshExpandedState();
        }
        
        protected override void CreateInputPorts() {
            base.CreateInputPorts();
            
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                InputParameterPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(ParameterNode));
            if(_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                InputParameterPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(ParameterNode));
            
            InputParameterPort.name = "";
            InputParameterPort.portName = "Parameter";
            inputContainer.Add(InputParameterPort);
        }

        public ChoiceNodeView(string pathUxml) : base(pathUxml) { }
    }
}