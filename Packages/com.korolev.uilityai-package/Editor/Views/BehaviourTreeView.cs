using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using BehaviourTree.Editor.VisualElements.Nodes;
using Node = BehaviourTree.Runtime.Nodes.Node;

namespace BehaviourTree.Editor.VisualElements
{
    internal class BehaviourTreeView : GraphView
    {
        private Runtime.BehaviourTree _tree;
        private IManipulator _selectionDragger;
        private IManipulator _rectangleSelector;
        private string NodeStyle
        {
            get
            {
                if (_tree == null) return Constants.DefaultNodeViewPath;
                return _tree.Orientation == Orientation.Horizontal 
                    ? Constants.HorizontalNodeViewPath 
                    : Constants.VerticalNodeViewPath;
            }
        }
        internal bool EnableRuntimeEdit => _tree.EnableRuntimeEdit;
        internal event Action<NodeView> OnNodeSelected;

        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            SetEnabled(true);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Constants.USSPath);
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.Where(endPort =>
                endPort.direction != startPort.direction    &&
                !endPort.node.Equals(startPort.node)        &&
                endPort.portType.Equals(startPort.portType)).ToList();
        }
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            BuildNodesCategory<Runtime.Nodes.Action>(evt, "Actions");
            BuildNodesCategory<Condition>(evt, "Conditions");
            BuildNodesCategory<CompositeNode>(evt, "Composites");
            BuildNodesCategory<Decorator>(evt, "Decorators");
            evt.menu.AppendSeparator();
            BuildNodeCustomCategories(evt);
        }
        private void BuildNodeCustomCategories(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<Node>().Where(type => !type.IsAbstract);
            foreach (Type type in types)
            {
                foreach (var attr in type.GetCustomAttributes<CategoryAttribute>(false))
                {
                    evt.menu.AppendAction($"{attr.Category}/{ParseTypeToDisplayName(type)}", (action) =>
                    {
                        Vector2 mouseScreenPosition = evt.localMousePosition;
                        CreateNode(type, ref mouseScreenPosition);
                    });
                }
            }
        }
        private void BuildNodesCategory<T>(ContextualMenuPopulateEvent evt, string categoryName) where T : Node
        {
            var types = TypeCache.GetTypesDerivedFrom<T>().Where(type => !type.IsAbstract && type.BaseType == typeof(T) && type.GetCustomAttribute<CategoryAttribute>() == null);            
            foreach (Type type in types)
            {
                evt.menu.AppendAction($"{categoryName}/{ParseTypeToDisplayName(type)}", (action) =>
                {
                    Vector2 mouseScreenPosition = evt.localMousePosition;
                    CreateNode(type, ref mouseScreenPosition);
                });
            }
        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                OnElementsToRemove(ref graphViewChange);
            }

            if (graphViewChange.edgesToCreate != null)
            {
                OnEdgesToCreate(ref graphViewChange);
            }

            if (graphViewChange.movedElements != null)
            {
                OnElementsMoved(ref graphViewChange);
            }

            return graphViewChange;
        }
        private void OnUndoRedo()
        {
            PopulateView(_tree);
            AssetDatabase.SaveAssets();
        }
        private void OnElementsToRemove(ref GraphViewChange graphViewChange)
        {
            graphViewChange.elementsToRemove.ForEach(element =>
            {
                if (element is NodeView view)
                {
                    _tree.RemoveNode(view.Node);
                }

                if (element is Edge edge)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    parentView.Node.RemoveChild(childView.Node);
                }
            });
        }
        private void OnEdgesToCreate(ref GraphViewChange graphViewChange)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                parentView.Node.AddChild(childView.Node);
            });
        }
        private void OnElementsMoved(ref GraphViewChange graphViewChange)
        {
            graphViewChange.movedElements.ForEach(node =>
            {
                NodeView view = node as NodeView;
                view.SortChildren();
            });
        }

        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.GUID) as NodeView;
        }
        private string ParseTypeToDisplayName(Type type)
        {
            string name = type.Name;
            int i = 0;
            while (i < name.Length - 1)
            {
                if (char.IsUpper(name[i + 1]))
                {
                    name = name.Insert(i + 1, " ");
                    i++;
                }
                i++;
            }
            return name;
        }
        private void CreateNode(Type type, ref Vector2 position)
        {
            Node node = _tree.CreateNode(type, ParseTypeToDisplayName(type));
            CreateNodeView(node, ref position);
        }
        private void CreateNodeView(Node node) => CreateNodeView(node, ref node.Position);
        private void CreateNodeView(Node node, ref Vector2 position)
        {
            if (node == null) return;
            NodeView nodeView = new NodeView(node, _tree.Orientation, NodeStyle);
            nodeView.SetPosition(new Rect(position, nodeView.contentRect.size));
            nodeView.OnNodeSelected += OnNodeSelected;
            AddElement(nodeView);
        }
        private void CreateNodeEdge(Node node)
        {
            IEnumerable<Node> children = node.GetChildren();
            if (node == null || children == null || children.Count() == 0) return;

            NodeView parentView = FindNodeView(node);
            foreach (Node child in children)
            {
                NodeView childView = FindNodeView(child);
                Edge edge = parentView.OutputPort.ConnectTo(childView.InputPort);
                AddElement(edge);
            }
        }
        private void SaveTree(Runtime.BehaviourTree tree)
        {
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        internal new void SetEnabled(bool isRuntime)
        {
            if (!isRuntime)
            {
                this.RemoveManipulator(_selectionDragger);
                this.RemoveManipulator(_rectangleSelector);
                _selectionDragger = null;
                _rectangleSelector = null;
            }
            else
            {
                _selectionDragger = new SelectionDragger();
                this.AddManipulator(_selectionDragger);
                _rectangleSelector = new RectangleSelector();
                this.AddManipulator(_rectangleSelector);
            }
        }
        internal void UpdateNodesState()
        {
            nodes.ForEach(node =>
            {
                NodeView view = node as NodeView;
                view.UpdateState();
            });
        }
        internal void PopulateView(Runtime.BehaviourTree tree)
        {
            if (tree == null) return;

            _tree = tree;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (_tree.RootNode == null)
            {
                _tree.RootNode = _tree.CreateNode(typeof(Root), ParseTypeToDisplayName(typeof(Root)));
                SaveTree(_tree);
            }

            try
            {
                // Create Node Views
                foreach (var node in tree.Nodes) CreateNodeView(node);

                // Create Node Edges
                foreach (var node in tree.Nodes) CreateNodeEdge(node);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}