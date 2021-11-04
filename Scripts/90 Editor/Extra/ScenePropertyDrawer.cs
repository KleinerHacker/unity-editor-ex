using ExtendedEditor.Runtime.editor_ex.Scripts._00_Runtime.Extra;
using UnityEditor;
using UnityEngine;

namespace ExtendedEditor.Editor.editor_ex.Scripts._90_Editor.Extra
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public sealed class ScenePropertyDrawer : ExtendedDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);
            
            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;
            
            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                property.stringValue = newPath;
            }
        }
    }
}