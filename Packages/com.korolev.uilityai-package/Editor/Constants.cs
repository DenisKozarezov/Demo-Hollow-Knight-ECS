namespace BehaviourTree.Editor
{
    internal static class Constants
    {
        private const string AssemblyPath = "Packages/com.korolev.uilityai-package/Editor/";
        public const string WindowTitle = "Behaviour Tree Editor";
        public const string InputPort = "In";
        public const string OutputPort = "Out";
        public static string UXMLPath => string.Concat(AssemblyPath, "Styles/BehaviourTreeEditor.uxml");
        public static string USSPath => string.Concat(AssemblyPath, "Styles/BehaviourTreeEditor.uss");
        public const string DefaultNodeViewPath = "UXML/GraphView/Node.uxml";
        public static string HorizontalNodeViewPath => string.Concat(AssemblyPath, "Styles/HorizontalNodeView.uxml");
        public static string VerticalNodeViewPath => string.Concat(AssemblyPath, "Styles/VerticalNodeView.uxml");
    }
}
