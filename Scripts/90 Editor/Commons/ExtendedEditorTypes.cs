using System;

namespace UnityEditorEx.Editor.editor_ex.Scripts._90_Editor.Commons
{
    public sealed class TabItem
    {
        public string Title { get; }
        public Action OnGUI { get; }

        public TabItem(string title, Action onGui)
        {
            Title = title;
            OnGUI = onGui;
        }
    }
}