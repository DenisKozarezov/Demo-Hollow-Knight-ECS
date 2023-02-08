using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using BehaviourTree.Runtime;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Node = BehaviourTree.Runtime.Nodes.Node;

namespace BehaviourTree.Editor.VisualElements.Nodes
{
    internal class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        internal readonly Node Node;
        private readonly Orientation _orientation;
        private Port _inputPort;
        private Port _outputPort;
        private LinkedList<Port> _dymamicInputPorts = new LinkedList<Port>();
        private LinkedList<Port> _dymamicOutputPorts = new LinkedList<Port>();
        internal Port InputPort => _inputPort;
        internal Port OutputPort => _outputPort;

        internal event Action<NodeView> OnNodeSelected;

        internal NodeView(Node node, Orientation orientation, string stylePath) : base(stylePath)
        {
            Node = node;
            _orientation = orientation;
            title = node.Name;
            viewDataKey = node.GUID;
            style.left = node.Position.x;
            style.top = node.Position.y;

            SetupClasses();
            CreateInputPorts();
            CreateOutputPorts();
        }

        private void SetupClasses()
        {
            if (Node is Runtime.Nodes.Action) AddToClassList("action");
            if (Node is Condition) AddToClassList("condition");
            if (Node is Composite) AddToClassList("composite");
            if (Node is Decorator)
            {
                if (Node is Root) AddToClassList("root");
                else AddToClassList("decorator");
            }
        }
        private void CreateInputPorts()
        {
            if (Node is not Root)
            {
                _dymamicInputPorts = CreateDynamicInputPorts();
                _inputPort = InstantiatePort(_orientation, Direction.Input, Port.Capacity.Single, typeof(Node));
            }

            if (_inputPort == null) return;
            _inputPort.portName = Constants.InputPort;
            _inputPort.name = "port";
            inputContainer.Add(_inputPort);
        }
        private void CreateOutputPorts()
        {
            Port.Capacity capacity = Port.Capacity.Single;
            if (Node is Composite)
                capacity = Port.Capacity.Multi;

            // Action Nodes don't have children (output ports)
            if (Node is not Runtime.Nodes.Action)
            {
                _dymamicOutputPorts = CreateDynamicOutputPorts();
                _outputPort = InstantiatePort(_orientation, Direction.Output, capacity, typeof(Node));
            }

            if (_outputPort == null) return;
            _outputPort.portName = Constants.OutputPort;
            _outputPort.name = "port";
            outputContainer.Add(_outputPort);
        }
        private LinkedList<Port> CreateDynamicInputPorts()
        {
            var fields = GetBackingFields<Runtime.Nodes.InputAttribute>();
            if (fields.Count() == 0) return null;
            foreach (FieldInfo field in fields)
            {
                Runtime.Nodes.InputAttribute attr = field.GetCustomAttribute<Runtime.Nodes.InputAttribute>();
                Port.Capacity capacity = attr.ConnectionType == PortConnection.Single ? Port.Capacity.Single : Port.Capacity.Multi;
                Port port = InstantiatePort(_orientation, Direction.Input, capacity, field.FieldType);
                port.name = "port";
                port.portName = field.Name;
                _dymamicInputPorts.AddLast(port);
                inputContainer.Add(port);
            }
            return _dymamicInputPorts;
        }
        private LinkedList<Port> CreateDynamicOutputPorts()
        {
            var fields = GetBackingFields<OutputAttribute>();
            if (fields.Count() == 0) return null;
            foreach (FieldInfo field in fields)
            {
                OutputAttribute attr = field.GetCustomAttribute<OutputAttribute>();
                Port.Capacity capacity = attr.ConnectionType == PortConnection.Single ? Port.Capacity.Single : Port.Capacity.Multi;
                Port port = InstantiatePort(_orientation, Direction.Output, capacity, field.FieldType);
                port.name = "port";
                port.portName = field.Name;
                _dymamicOutputPorts.AddLast(port);
                outputContainer.Add(port);
            }
            return _dymamicOutputPorts;
        }
        private IEnumerable<FieldInfo> GetBackingFields<T>() where T : Attribute
        {
            return Node.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => Attribute.IsDefined(field, typeof(T))).Reverse();
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(Node, "Move Node (Behaviour Tree)");
            Node.Position.x = newPos.xMin;
            Node.Position.y = newPos.yMin;
            EditorUtility.SetDirty(Node);
        }
        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
        internal void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("success");
            RemoveFromClassList("failure");

            if (EditorApplication.isPlaying)
            {
                switch (Node.State)
                {
                    case State.Running:
                        if (Node.Started) AddToClassList("running");
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
        internal void SortChildren()
        {
            if (Node as Composite is var node)
            {
                node?.SortChildren(
                    _orientation == Orientation.Horizontal
                    ? SortByVerticalPosition
                    : SortByHorizontalPosition);
            }
        }
        private int SortByHorizontalPosition(Node left, Node right)
        {
            return left.Position.x < right.Position.x ? -1 : 1;
        }
        private int SortByVerticalPosition(Node left, Node right)
        {
            return left.Position.y < right.Position.y ? -1 : 1;
        }
    }
}