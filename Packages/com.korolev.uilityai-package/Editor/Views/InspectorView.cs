using UnityEngine.UIElements;
using BehaviourTree.Editor.VisualElements.Nodes;

namespace BehaviourTree.Editor.VisualElements
{
    internal class InspectorView : VisualElement
    {
        private UnityEditor.Editor _editor;
        public new class UXMLFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        internal void UpdateSelection(NodeView nodeView)
        {
            if (nodeView == null) return;

            Clear();

            UnityEngine.Object.DestroyImmediate(_editor);
            _editor = UnityEditor.Editor.CreateEditor(nodeView.Node);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (_editor.target) _editor?.OnInspectorGUI();
            });
            Add(container);
        }
    }
}