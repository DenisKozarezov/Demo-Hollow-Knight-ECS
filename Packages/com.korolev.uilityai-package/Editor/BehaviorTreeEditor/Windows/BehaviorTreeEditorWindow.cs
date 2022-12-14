/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/

using AI.BehaviorTree;
using Editor.BehaviorTreeEditor.Config;
using Editor.BehaviorTreeEditor.VisualElements;
using Editor.BehaviorTreeEditor.VisualElements.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;


public class BehaviorTreeEditorWindow : EditorWindow
{
    private static BehaviorTreeView _behaviorTreeView;
    private static ToolbarMenuThemeStyle _themeStyle;
    private InspectorView _inspectorView;
    private static StyleThemeConfig _styleThemeConfig;
    
    [MenuItem("UtilityAI/BehaviorTreeEditorWindow...")]
    public static void CreateWindow() 
    {
        BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditorWindow");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is BehaviorTree)
        {
            CreateWindow();
            
            BehaviorTree behaviorTree = Selection.activeObject as BehaviorTree;
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

    private void CreateGUI() 
    {
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
            SetLightTheme();
            SaveAsset(_styleThemeConfig, ThemeStyle.LightTheme);
        });
        _themeStyle.menu.AppendAction("Dark Theme", (i) =>
        {
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
    private void OnInspectorUpdate()
    {
        _behaviorTreeView?.UpdateNodesStates();
    }

    private void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspectorView.UpdateSelection(nodeView);
    }
    private void OnNodeUnselectionChanged()
    {
        _inspectorView.UpdateSelection(null);
    }    
    private void SetDarkTheme()
    {
        rootVisualElement.ClearClassList();
        rootVisualElement.AddToClassList("dark-theme");

        _inspectorView.SetDarkTheme();
        _behaviorTreeView.SetDarkTheme();
    }
    private void SetLightTheme()
    {
        rootVisualElement.ClearClassList();
        rootVisualElement.AddToClassList("light-theme");
        
        _inspectorView.SetLightTheme();
        _behaviorTreeView.SetLightTheme();
    }    
    private void SaveAsset(Object asset, ThemeStyle theme)
    {
        ((StyleThemeConfig)asset).Theme = theme;
        
        AssetDatabase.SaveAssetIfDirty(asset);
        EditorUtility.SetDirty(asset);
    }
}