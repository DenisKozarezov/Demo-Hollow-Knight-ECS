using System;
using System.Collections.Generic;
using AI.BehaviourTree.Nodes;
using AI.BehaviourTree.Nodes.Decorators;
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
        private Texture2D _identificationIcon;

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            _behaviorTreeView.CreateNodeForView((Type)SearchTreeEntry.userData, _behaviorTreeView.GetLocalMousePosition(context.screenMousePosition, true));
            return true;
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
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identificationIcon))
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
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identificationIcon))
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
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", _identificationIcon))
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
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}",  _identificationIcon))
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
                    _searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}",  _identificationIcon))
                        {
                            userData = type, level = 2
                        }
                    );
                }  
            }
            return _searchTreeEntries;
        }
        public void Initialize(BehaviorTreeView behaviorTreeView)
        {
            _behaviorTreeView = behaviorTreeView;
            _identificationIcon = new Texture2D(1, 1);
            _identificationIcon.SetPixel(0,0, Color.clear);
            _identificationIcon.Apply();
        }        
    }
}