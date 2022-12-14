using Editor.BehaviorTreeEditor.VisualElements.Nodes;
using UnityEngine.UIElements;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class InspectorView : VisualElement
    {
        private UnityEditor.Editor _editor;
        public new class UXMLFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }
                
        public void UpdateSelection(NodeView nodeView)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);
            if (nodeView != null)
            {
                _editor = UnityEditor.Editor.CreateEditor(nodeView.Node);
                IMGUIContainer imguiContainer = new IMGUIContainer(() => { _editor.OnInspectorGUI(); });
                Add(imguiContainer);
            }
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