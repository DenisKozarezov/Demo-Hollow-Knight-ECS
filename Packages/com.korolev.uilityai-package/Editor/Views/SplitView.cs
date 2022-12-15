using UnityEngine.UIElements;

namespace BehaviourTree.Editor.VisualElements
{
    internal class SplitView: TwoPaneSplitView
    {
        public new class UXMLFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
    }
}