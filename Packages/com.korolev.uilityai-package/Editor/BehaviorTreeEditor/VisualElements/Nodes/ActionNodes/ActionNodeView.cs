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
    public class ActionNodeView: NodeView
    {
        public override void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected) {
            base.Initialize(node, behaviorTreeView, position, onNodeSelected, onNodeUnselected);
        }

        public override void Draw() {
            /*TITLE CONTAINER*/
            this.title = Node.name;

            /*TITLE EXTENSION*/

            CreateInputPorts();
            RefreshPorts();
            RefreshExpandedState();
        }

        public ActionNodeView(string pathUxml) : base(pathUxml) { }
    }
}