/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using AI.BehaviorTree;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.ActionNodes;
using AI.BehaviorTree.Nodes.CompositeNodes;
using AI.BehaviorTree.Nodes.DecoratorNodes;
using AI.BehaviorTree.Nodes.ParameterNodes;
using Editor.BehaviorTreeEditor.VisualElements.Nodes;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Actions;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Choices;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Composites;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Conditions;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Decorators;
using Editor.BehaviorTreeEditor.VisualElements.Nodes.Parameters;
using Editor.BehaviorTreeEditor.Windows;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = AI.BehaviorTree.Nodes.Node;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class BehaviorTreeView : GraphView
    {       
        public new class UXMLFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { }
        
        private BehaviorTree _behaviorTree;
        private NodeSearchWindow _nodeSearchWindow;   
        
        public Action<NodeView> OnNodeSelected;
        public Action OnNodeUnselected;
        public Action<NodeView> OnGroupSelected;
        public Action OnGroupUnselected;
        public TreeOrientation OrientationTree;

        public BehaviorTreeEditorWindow BehaviorTreeEditorWindow { get; set; }
        public const string StylePath = "Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/Styles/BehaviorTreeViewStyle.uss";
        
        public BehaviorTreeView() 
        {
            Insert(0, new GridBackground());
            AddManipulators();
            AddSearchWindow();

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylePath);
            styleSheets.Add(styleSheet);
        }

        public IManipulator UnGroupContextualManipulator()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("UnGroup", (actionEvent) =>
                    {
                        foreach (var elem in selection)
                        {
                            NodeView nodeView = elem as NodeView;
                            if (nodeView != null)
                            {
                                GroupView groupView = FindGroupView(nodeView.Node.GroupSo.GUID);
                                groupView.RemoveElement(nodeView);
                                groupView.GroupSO.Nodes.Remove(nodeView.Node);
                                nodeView.Node.GroupSo = null;
                            }
                        }
                    }
                ));
            return contextualMenuManipulator;
        }
        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group", (actionEvent) =>
                    {
                        GroupSO groupSo = CreateGroupBeforeView("Group", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition));
                        AddElement(CreateGroupForView(groupSo));
                    }
                ));
            return contextualMenuManipulator;
        }
        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => BuildContextualMenu(menuEvent)
            );
            return contextualMenuManipulator;
        }
        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu());
            this.AddManipulator(CreateGroupContextualMenu());
        }
        private void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            
        }
        private GroupSO CreateGroupBeforeView(string title, Vector2 localMousePosition)
        {
            GroupSO groupSo = _behaviorTree.CreateGroup(title);
            groupSo.Position = localMousePosition;
            return groupSo;
        }
        private GroupView CreateGroupForView(GroupSO groupSo)
        {
            GroupView groupView = new GroupView();
            groupView.Initialize(groupSo, this, groupSo.Position, OnGroupSelected, OnGroupUnselected);
            groupView.SetPosition(new Rect(groupSo.Position, Vector2.zero));
            return groupView;
        }        
        private NodeView CreateNodeViewBeforeDraw(Node node, Vector2 position)
        {
            NodeView nodeView = null;
            switch (node)
            {
                case DebugLogNode:
                    nodeView = new DebugLogNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case SequencerNode:
                    nodeView = new SequencerNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case RepeatNode:
                    nodeView = new RepeatNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case ChoiceNode:
                    nodeView = new ChoiceNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case FloatNode:
                    nodeView = new FloatNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case BooleanNode:
                    nodeView = new BooleanNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case ConditionNode:
                    nodeView = new ConditionNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case WaitNode:
                    nodeView = new WaitNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case RootNode:
                    nodeView = new RootNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
                case ActionNode:
                    nodeView = new ActionNodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");
                    break;
            }
            
            if(nodeView == null)
                nodeView = new NodeView("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/NodeView.uxml");

            nodeView.Initialize(node, this, position, OnNodeSelected, OnNodeUnselected);
            return nodeView;
        }
        public NodeView FindNodeView(Node node) 
        {
            return GetNodeByGuid(node.GUID) as NodeView;
        }
        private GroupSO FindGroupSO(string guid)
        {
            return _behaviorTree.Groups.Find(i => i.GUID == guid) as GroupSO;
        }
        private GroupView FindGroupView(string guid)
        {
            return GetElementByGuid(guid) as GroupView;
        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
        {
            if (_behaviorTree != null) {
                // When remove nodes
                if (graphviewchange.elementsToRemove != null) {
                    graphviewchange.elementsToRemove.ForEach(elem => {
                        if (elem is Edge edge)
                        {
                            NodeView parentNode = edge.output.node as NodeView;
                            NodeView childNode = edge.input.node as NodeView;
                            _behaviorTree.RemoveChild(parentNode.Node,childNode.Node);
                        }
                        
                        if (elem is NodeView nodeView) _behaviorTree.RemoveNode(nodeView.Node);
                        if (elem is GroupView group) _behaviorTree.RemoveGroup(FindGroupSO(group.viewDataKey));
                    });
                    _behaviorTree.BehaviorTreeChanged?.Invoke();
                }
                // New edges to create
                if (graphviewchange.edgesToCreate != null)
                {
                    graphviewchange.edgesToCreate.ForEach(edge =>
                    {
                        NodeView parentNode = edge.output.node as NodeView;
                        NodeView childNode = edge.input.node as NodeView;
                        _behaviorTree.AddChild(parentNode.Node, childNode.Node);
                    });
                    _behaviorTree.BehaviorTreeChanged?.Invoke();
                }
            }
            return graphviewchange;
        }         
        private void DrawNodeView(NodeView nodeView) 
        {
            AddElement(nodeView);
            nodeView.Draw();
        }  
        private void AddSearchWindow()
        {
            nodeCreationRequest = context =>
            {
                _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
                _nodeSearchWindow.Initialize(this);
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _nodeSearchWindow);
            };
        }
        public Node CreateNodeForView(Type type, Vector2 position)
        {
            Node node = _behaviorTree.CreateNode(type); // в дереве есть логика создания узла
            var nodeView = CreateNodeViewBeforeDraw(node, position);
            DrawNodeView(nodeView);
            return node;
        }
        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = BehaviorTreeEditorWindow.rootVisualElement.ChangeCoordinatesTo(BehaviorTreeEditorWindow.rootVisualElement.parent, mousePosition - BehaviorTreeEditorWindow.position.position);
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction && endPort.node != startPort.node && endPort.portType == startPort.portType).ToList();
        }               
        public void UpdateNodesStates()
        {
            nodes.ForEach(n =>
            {
                NodeView nodeView = n as NodeView;
                nodeView.UpdateState();
            });
        }
        public void DrawSelectionTreeView(BehaviorTree tree, TreeOrientation orientationTree = TreeOrientation.Horizontal)
        {
            OrientationTree = orientationTree;
            _behaviorTree = tree;
            if (_behaviorTree != null)
            {
                graphViewChanged -= OnGraphViewChanged;
                DeleteElements(graphElements);
                graphViewChanged += OnGraphViewChanged;

                // Drawing all nodes
                _behaviorTree.Nodes.ForEach(node =>
                {
                    var nodeView = CreateNodeViewBeforeDraw(node, node.Position);
                    DrawNodeView(nodeView);
                }); 
                
                // Drawing all edges
                _behaviorTree.Nodes.ForEach(node =>
                {
                    IEnumerable<Node> children = node.GetChildren();
                    if (children == null || !children.Any() || children.Any(x => x is null)) return;

                    foreach (Node child in children)
                    {
                        NodeView parentNodeView = FindNodeView(node);
                        NodeView childNodeView = FindNodeView(child);

                        Edge edge;
                        if (parentNodeView is ChoiceNodeView)
                        {
                            foreach (var parameterNode in ((ChoiceNode)parentNodeView.Node).ParametersList)
                            {
                                edge = FindNodeView(parameterNode).OutputPort.ConnectTo(((ChoiceNodeView)parentNodeView).InputParameterPort);
                                AddElement(edge);
                            }
                        }

                        edge = null;

                        if ((parentNodeView is FloatNodeView || parentNodeView is BooleanNodeView) && childNodeView is ChoiceNodeView)
                            edge = parentNodeView.OutputPort.ConnectTo(((ChoiceNodeView)childNodeView).InputParameterPort);
                        else if (parentNodeView is ConditionNodeView && childNodeView is RepeatNodeView)
                            edge = parentNodeView.OutputPort.ConnectTo(((RepeatNodeView)childNodeView).InputConpitionPort);
                        else
                            edge = parentNodeView.OutputPort.ConnectTo(childNodeView.InputPort);

                        AddElement(edge);
                    }
                });
                
                // Drawing Root Node
                if (_behaviorTree.RootNode == null)
                {
                    _behaviorTree.RootNode = CreateNodeForView(typeof(RootNode), Vector2.zero);
                }
                
                // Drawing all groups
                _behaviorTree.Groups.ForEach(groupSo =>
                {
                    GroupView groupView = CreateGroupForView(groupSo);
                    AddElement(groupView);
                });
            }
            else DeleteElements(graphElements);
        }
        public void SetDarkTheme()
        {
            this.ClearClassList();
            this.AddToClassList("dark-theme");
        }
        public void SetLightTheme()
        {
            this.ClearClassList();
            this.AddToClassList("light-theme");
        }
    }
}