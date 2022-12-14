using System;
using System.Collections.Generic;
using System.Linq;
using AI.BehaviourTree.Nodes;
using Editor.BehaviorTreeEditor.VisualElements.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class GroupView : Group
    {
        protected BehaviorTreeView _behaviorTreeView;        
        public GroupSO GroupSO;
        public Action<NodeView> OnGroupSelected;
        public Action OnGroupUnselected;        

        public virtual void Initialize(GroupSO groupSo, BehaviorTreeView behaviorTreeView, Vector2 position, Action<NodeView> onGroupSelected, Action onGroupUnselected) 
        {
            GroupSO = groupSo;
            _behaviorTreeView = behaviorTreeView;
            viewDataKey = GroupSO.GUID;
            title = GroupSO.Title;
            
            SetPosition(new Rect(position, Vector2.zero));
            OnGroupSelected = onGroupSelected;
            OnGroupUnselected = onGroupUnselected;

            foreach (var node in GroupSO.Nodes)
            {
                AddElement(_behaviorTreeView.FindNodeView(node));
            }
            
            foreach (var elem in _behaviorTreeView.selection)
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    AddElement(nodeView);
                }
            }
        }

        protected override void OnGroupRenamed(string oldName, string newName)
        {
            base.OnGroupRenamed(oldName, newName);
            GroupSO.Title = title;
        }
        protected override void OnElementsAdded(IEnumerable<GraphElement> elements)
        {
            base.OnElementsAdded(elements);
            //если узел был добавлен в группу
            foreach (var element in elements)
            { 
                NodeView nodeView = element as NodeView;
                if (nodeView != null)
                {
                    nodeView.Node.GroupSo = GroupSO;
                    nodeView.Node.UnGroupManipulator = _behaviorTreeView.UnGroupContextualManipulator();
                    nodeView.AddManipulator(nodeView.Node.UnGroupManipulator);

                    var node = GroupSO.Nodes.FirstOrDefault(i => i.GUID == nodeView.Node.GUID);
                    if(node == null)
                        GroupSO.Nodes.Add(nodeView.Node);
                }
            }
        }
        protected override void OnElementsRemoved(IEnumerable<GraphElement> elements)
        {
            base.OnElementsRemoved(elements);
            foreach (var element in elements)
            {
                NodeView nodeView = element as NodeView;
                if (nodeView != null)
                {
                    nodeView.RemoveManipulator(nodeView.Node.UnGroupManipulator);
                }
            }
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            GroupSO.Position = new Vector2(newPos.xMin, newPos.yMin);
        }
    }
}