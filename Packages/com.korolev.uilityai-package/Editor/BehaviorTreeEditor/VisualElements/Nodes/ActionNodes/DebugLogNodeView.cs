/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using UnityEngine;
using Node = AI.BehaviourTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.Actions
{
    public class DebugLogNodeView : NodeView
    {        
        public override void Initialize(Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) 
        {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }
        public override void Draw() 
        {
            this.title = "DebugLog";

            CreateInputPorts();
            RefreshPorts();
            RefreshExpandedState();
        }

        public DebugLogNodeView(string pathUxml) : base(pathUxml) { }
    }
}