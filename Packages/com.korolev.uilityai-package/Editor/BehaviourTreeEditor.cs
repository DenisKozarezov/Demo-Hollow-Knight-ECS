using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using Leopotam.Ecs;
using BehaviourTree.Editor.VisualElements;
using BehaviourTree.Editor.VisualElements.Nodes;
using Core.ECS.Components.Units;

namespace BehaviourTree.Editor
{
    internal class BehaviourTreeEditor : EditorWindow
    {
        private BehaviourTreeView _behaviourView;
        private InspectorView _inspectorView;
        private VisualElement _root;

        [MenuItem("Behaviour Tree/Editor")]
        public static void OpenWindow()
        {
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            wnd.titleContent = new GUIContent(Constants.WindowTitle);
        }
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is Runtime.BehaviourTree)
            {
                OpenWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
        private void CreateGUI()
        {
            _root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Constants.UXMLPath);
            visualTree.CloneTree(_root);

            // Import USS
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Constants.USSPath);
            _root.styleSheets.Add(styleSheet);

            _behaviourView = _root.Q<BehaviourTreeView>();
            _inspectorView = _root.Q<InspectorView>();
            _behaviourView.OnNodeSelected += OnNodeSelectionChanged;
            OnSelectionChange();
        }
        private void OnSelectionChange()
        {
            // If double-click on scriptable object
            Runtime.BehaviourTree tree = Selection.activeObject as Runtime.BehaviourTree;
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _behaviourView?.PopulateView(tree);
            }

            // If click on some behaviour agent GO in playmode
            if (!EditorApplication.isPlaying) return;
            
            GameObject go = Selection.activeGameObject;
            if (go != null && go.TryGetComponent(out Core.ECS.EntityReference entityRef))
            {
                if (entityRef.Entity.Has<BehaviourTreeComponent>())
                {
                    ref var component = ref entityRef.Entity.Get<BehaviourTreeComponent>();
                    _behaviourView?.PopulateView(component.BehaviourTree);
                }
            }
        }
        private void OnInspectorUpdate()
        {
            _behaviourView?.UpdateNodesState();
        }
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    _behaviourView?.SetEnabled(true);
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    _behaviourView?.SetEnabled(false);
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    _behaviourView?.Clear();
                    break;
            }
        }
        private void OnNodeSelectionChanged(NodeView nodeView)
        {
            _inspectorView?.UpdateSelection(nodeView);
        }
    }
}