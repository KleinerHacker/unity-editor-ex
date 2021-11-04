using System.Linq;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts._00_Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts._90_Editor.Extra
{
    [CustomPropertyDrawer(typeof(AssetChooserAttribute))]
    public sealed class AssetChooserPropertyDrawer : ExtendedDrawer
    {
        private int _selection;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return lineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var assetChooser = (AssetChooserAttribute) attribute;
            var assetGuids = AssetDatabase.FindAssets("t:" + assetChooser.Type.Name);
            var assetPaths = assetGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
            var assetObjects = assetPaths.Select(x => AssetDatabase.LoadAssetAtPath(x, assetChooser.Type)).ToArray();

            var assetObject = property.objectReferenceValue;
            _selection = assetObjects.ToList().IndexOf(assetObject);
            _selection = EditorGUI.Popup(position, _selection, assetObjects.Select(x => x.name).ToArray());
            property.objectReferenceValue = assetObjects[_selection];
        }
    }
}