using System;
using System.Collections.Generic;
using AI.BehaviorTree.Nodes;
using AI.BehaviorTree.Nodes.DecoratorNodes;
using Editor.BehaviorTreeEditor.VisualElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.BehaviorTreeEditor.Windows
{
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private List<SearchTreeEntry> _searchTreeEntries;
        private BehaviorTreeView _behaviorTreeView;
        private Texture2D _identiationIcon;
        public void Initialize(BehaviorTreeView behaviorTreeView)
        {
            _behaviorTreeView = behaviorTreeView;
            _identiationIcon = new Texture2D(1, 1);
            _identiationIcon.SetPixel(0,0, Color.clear);
            _identiationIcon.Apply();
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            _searchTreeEntries = new List<SearchTreeEntry>();

            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Node")));
            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Action Node"), 1));

            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identiationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            
            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Composite Node"), 1));

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var type in types)
                {
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identiationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            
            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Condition Node"), 1));
            
            {
                var types = TypeCache.GetTypesDerivedFrom<ConditionNode>();
                foreach (var type in types)
                {
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identiationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            
            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Decorator Node"), 1));

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    if (type == typeof(RootNode)) continue;
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}",  _identiationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            
            _searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Parameter Node"), 1));
            
            {
                var types = TypeCache.GetTypesDerivedFrom<ParameterNode>();
                foreach (var type in types)
                {
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}",  _identiationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            return _searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            _behaviorTreeView.CreateNodeForView((Type)SearchTreeEntry.userData, _behaviorTreeView.GetLocalMousePosition(context.screenMousePosition, true));
            return true;
        }
    }
}