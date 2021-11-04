using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts._00_Runtime.Extra;
using UnityEditorInternal;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts._90_Editor.Extra
{
    [CustomPropertyDrawer(typeof(LayerMaskAttribute))]
    public sealed class LayerMaskPropertyDrawer : ExtendedDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var mask = property.intValue;
            mask = EditorGUI.MaskField(position, label, mask, InternalEditorUtility.layers);
            property.intValue = mask;
        }
    }
}