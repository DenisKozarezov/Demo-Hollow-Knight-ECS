/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using AI.BehaviourTree;
using AI.BehaviourTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes
{
    public class NodeView : Node
    {
        private string _name;
        protected BehaviorTreeView _behaviorTreeView;
        public AI.BehaviourTree.Nodes.Node Node { get; set; }
        public Port InputPort { get; set; }
        public Port OutputPort { get; set; }

        public Action<NodeView> OnNodeSelected;
        public Action OnNodeUnselected;

        public NodeView(string pathUxml) : base(pathUxml) { }

        protected virtual void CreateInputPorts()
        {
            if (_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            if (_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

            InputPort.name = "";
            InputPort.portName = "";
            inputContainer.Add(InputPort);
        }           
        protected virtual void CreateOutputPorts()
        {
            if (_behaviorTreeView.OrientationTree == TreeOrientation.Horizontal)
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            if (_behaviorTreeView.OrientationTree == TreeOrientation.Vertical)
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));

            OutputPort.name = "";
            OutputPort.portName = "";
            outputContainer.Add(OutputPort);
        }
        public virtual void Initialize(AI.BehaviourTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected)
        {
            Node = node;
            _behaviorTreeView = behaviorTreeView;
            viewDataKey = Node.GUID;
            _name = Node.name;
            SetPosition(new Rect(position, Vector2.zero));
            OnNodeSelected = onNodeSelected;
            OnNodeUnselected = onNodeUnselected;
        }
        public virtual void Draw()
        {
            this.title = _name;
            CreateInputPorts();
            CreateOutputPorts();
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.Position = new Vector2(newPos.xMin, newPos.yMin);
        }
        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
        public override void OnUnselected()
        {
            base.OnUnselected();
            OnNodeUnselected?.Invoke();
        }
        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("failure");
            RemoveFromClassList("success");
            if (Application.isPlaying)
            {
                switch (Node.State)
                {
                    case State.Running:
                        if (Node.Started)
                            AddToClassList("running");
                        break;
                    case State.Failure:
                        AddToClassList("failure");
                        break;
                    case State.Success:
                        AddToClassList("success");
                        break;
                }
            }
        }
    }
}