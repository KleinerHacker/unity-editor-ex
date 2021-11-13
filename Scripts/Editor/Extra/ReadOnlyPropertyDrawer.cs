using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Extra
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