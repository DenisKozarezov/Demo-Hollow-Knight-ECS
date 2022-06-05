using UnityEngine.UIElements;

namespace Editor.BehaviorTreeEditor.VisualElements
{
    public class SplitView: TwoPaneSplitView
    {
        //нужно для использования UXML 
        public new class UXMLFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
    }
}