using System;
using System.Reflection;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Extra
{
    [CustomPropertyDrawer(typeof(HideAttribute))]
    public sealed class HideDrawer : ConditionalDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (FindMethod(property) == null)
                return base.GetPropertyHeight(property, label);
            
            var success = InvokeMethod(property);
            if (!success) 
                return 0f;
            
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (FindMethod(property) == null)
                return;
            
            var success = InvokeMethod(property);
            if (!success)
                return;

            EditorGUI.PropertyField(position, property, label);
        }
    }
}