/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Linq;
using AI.BehaviorTree;
using AI.BehaviorTree.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace Editor.BehaviorTreeEditor.VisualElements.Nodes
{
    public class NodeView : Node
    {
        public Action<NodeView> OnNodeSelected;
        public Action OnNodeUnselected;
        public string NodeName { get; set; }
        protected BehaviorTreeView _behaviorTreeView;
        public AI.BehaviorTree.Nodes.Node Node { get; set; }
        public Port InputPort { get; set; }
        public Port OutputPort { get; set; }

        public NodeView(string pathUxml) : base(pathUxml) { }

        public virtual void Initialize(AI.BehaviorTree.Nodes.Node node, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onNodeSelected, Action onNodeUnselected)
        {
            Node = node;
            _behaviorTreeView = behaviorTreeView;
            viewDataKey = Node.GUID;
            NodeName = Node.name;
            SetPosition(new Rect(position, Vector2.zero));
            OnNodeSelected = onNodeSelected;
            OnNodeUnselected = onNodeUnselected;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.Position = new Vector2(newPos.xMin, newPos.yMin);
        }

        public virtual void Draw()
        {
            /*TITLE CONTAINER*/
            this.title = NodeName;

            CreateInputPorts();
            CreateOutputPorts();
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

        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (!port.connected)
                {
                    continue;
                }
                _behaviorTreeView.DeleteElements(port.connections);
            }
        }

        public bool IsStartingNode()
        {
            Port inputPort = (Port)inputContainer.Children().First();
            return !inputPort.connected;
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