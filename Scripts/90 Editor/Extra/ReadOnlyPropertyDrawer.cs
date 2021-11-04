using ExtendedEditor.Runtime.editor_ex.Scripts._00_Runtime.Extra;
using UnityEditor;
using UnityEngine;

namespace ExtendedEditor.Editor.editor_ex.Scripts._90_Editor.Extra
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyPropertyDrawer : ExtendedDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}