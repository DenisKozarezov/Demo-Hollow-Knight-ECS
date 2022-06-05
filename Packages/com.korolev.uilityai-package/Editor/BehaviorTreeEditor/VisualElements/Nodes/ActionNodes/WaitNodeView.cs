/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = AI.BehaviorTree.Nodes.Node;
namespace Editor.BehaviorTreeEditor.VisualElements.Nodes.ActionNodes
{
    public class WaitNodeView : NodeView
    {
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = "Wait";

            /*TITLE EXTENSION*/
            CreateInputPorts();
            RefreshPorts();
            RefreshExpandedState();
        }

        public WaitNodeView(string pathUxml) : base(pathUxml) { }
    }
}