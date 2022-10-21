using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Extra
{
    [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
    public sealed class SortingLayerDrawer : ExtendedDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var layerNames = SortingLayer.layers
                .Select(x => new GUIContent(x.name))
                .ToArray();
            var selected = layerNames.IndexOf(x => x.text == property.stringValue);

            var newSelected = EditorGUI.Popup(position, label, selected, layerNames);
            if (newSelected != selected)
            {
                if (newSelected < 0 || newSelected >= SortingLayer.layers.Length)
                {
                    property.stringValue = "";
                }
                else
                {
                    property.stringValue = layerNames[newSelected].text;
                }
            }
        }
    }
}