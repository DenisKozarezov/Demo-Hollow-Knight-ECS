/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using UnityEngine;
using UnityEngine.UIElements;
using Node = AI.BehaviorTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.ActionNodes
{
    public class DebugLogNodeView : NodeView
    {
        private TextField textFieldMessage = new TextField();
        
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = "DebugLog";

            CreateInputPorts();
            RefreshPorts();
            RefreshExpandedState();
        }

        public DebugLogNodeView(string pathUxml) : base(pathUxml) { }
    }
}