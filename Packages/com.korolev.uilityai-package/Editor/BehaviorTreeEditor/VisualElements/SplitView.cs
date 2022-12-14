using UnityEngine.UIElements;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class SplitView: TwoPaneSplitView
    {
        public new class UXMLFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
    }
}