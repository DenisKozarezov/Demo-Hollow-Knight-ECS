/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using System;
using AI.BehaviorTree;
using AI.ECS;
using AI.ECS.Components;
using Editor.BehaviorTreeEditor.Config;
using Editor.BehaviorTreeEditor.VisualElements;
using Editor.BehaviorTreeEditor.VisualElements.Nodes;
using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;


public class BehaviorTreeEditorWindow : EditorWindow
{
    private static BehaviorTreeView _behaviorTreeView;  //ссылка на элемент, в котором рисуется дерево
    private static ToolbarMenuThemeStyle _themeStyle;
    private InspectorView _inspectorView;
    private static StyleThemeConfig _styleThemeConfig;
    
    [MenuItem("UtilityAI/BehaviorTreeEditorWindow...")]
    public static void CreateWindow() {
        BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditorWindow");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is BehaviorTree)
        {
            CreateWindow();
            
            BehaviorTree behaviorTree = Selection.activeObject as BehaviorTree; // получили BehaviorTree, который выбрали мышкой
            if (behaviorTree != null)
            {
                var containsAsset = AssetDatabase.Contains(behaviorTree);
                if (containsAsset)
                    _behaviorTreeView.DrawSelectionTreeView(behaviorTree, behaviorTree.OrientationTree);
                else 
                    _behaviorTreeView.DrawSelectionTreeView(null);
            }
            else _behaviorTreeView.DrawSelectionTreeView(null);
            
            return true;
        }
        return false;
    }

    public void CreateGUI() {
        _styleThemeConfig = AssetDatabase.LoadAssetAtPath<StyleThemeConfig>("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/Config/StyleThemeConfig.asset");
        
        VisualElement root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/UXML/BehaviorTreeEditorWindow.uxml");
        visualTree.CloneTree(root);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.korolev.uilityai-package/Editor/BehaviorTreeEditor/Styles/BehaviorTreeEditorWindow.uss");
        root.styleSheets.Add(styleSheet);

        _behaviorTreeView = root.Q<BehaviorTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _behaviorTreeView.BehaviorTreeEditorWindow = this;
        _themeStyle = root.Q<ToolbarMenuThemeStyle>();
        
        _themeStyle.menu.AppendAction("Light Theme", (i) =>
        {
            Debug.Log("Сделана светлая тема");
            SetLightTheme();
            SaveAsset(_styleThemeConfig, ThemeStyle.LightTheme);
        });
        _themeStyle.menu.AppendAction("Dark Theme", (i) =>
        {
            Debug.Log("Сделана темная тема");
            SetDarkTheme();
            SaveAsset(_styleThemeConfig, ThemeStyle.DarkTheme);
        });
        _behaviorTreeView.OnNodeSelected = OnNodeSelectionChanged;
        _behaviorTreeView.OnNodeUnselected = OnNodeUnselectionChanged;
        _behaviorTreeView.StretchToParentSize();

        if (_styleThemeConfig.Theme == ThemeStyle.LightTheme) 
            SetLightTheme();
        else
            SetDarkTheme();
    }

    private void OnSelectionChange()
    {
        BehaviorTree behaviorTree;

        if (Application.isPlaying)
        {
            GameObject gameObject = Selection.activeObject as GameObject;
            if (gameObject != null)
            {
                EntityReference entityReference = gameObject.GetComponent<EntityReference>();
                if (entityReference != null)
                {
                    EcsEntity ecsEntity = entityReference.Entity;
                    if (ecsEntity.Has<BehaviorTreeComponent>())
                    {
                        ref var behaviorTreeComponent = ref ecsEntity.Get<BehaviorTreeComponent>();
                        behaviorTree = behaviorTreeComponent.BehaviorTree;
                        _behaviorTreeView.DrawSelectionTreeView(behaviorTree, behaviorTree.OrientationTree);
                        return;
                    }
                }
            }
        }

        behaviorTree = Selection.activeObject as BehaviorTree; // получили BehaviorTree, который выбрали мышкой
        if (behaviorTree != null)
        {
            var containsAsset = AssetDatabase.Contains(behaviorTree);
            if (containsAsset)
                _behaviorTreeView.DrawSelectionTreeView(behaviorTree, behaviorTree.OrientationTree);
            else 
                _behaviorTreeView.DrawSelectionTreeView(null);
        }
        else _behaviorTreeView.DrawSelectionTreeView(null);
    }

    private void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspectorView.UpdateSelection(nodeView);
    }

    private void OnNodeUnselectionChanged()
    {
        _inspectorView.UpdateSelection(null);
    }

    private void OnInspectorUpdate()
    {
        _behaviorTreeView?.UpdateNodesStates();
    }
    
    public void SetDarkTheme()
    {
        rootVisualElement.ClearClassList();
        rootVisualElement.AddToClassList("dark-theme");

        _inspectorView.SetDarkTheme();
        _behaviorTreeView.SetDarkTheme();
    }
    
    public void SetLightTheme()
    {
        rootVisualElement.ClearClassList();
        rootVisualElement.AddToClassList("light-theme");
        
        _inspectorView.SetLightTheme();
        _behaviorTreeView.SetLightTheme();
    }
    
    private void SaveAsset(UnityEngine.Object asset, ThemeStyle theme)
    {
        ((StyleThemeConfig) asset).Theme = theme;
        
        AssetDatabase.SaveAssetIfDirty(asset);
        EditorUtility.SetDirty(asset);
    }
}