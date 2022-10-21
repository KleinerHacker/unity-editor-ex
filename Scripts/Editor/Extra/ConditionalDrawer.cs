using System;
using System.Reflection;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Extra
{
    public abstract class ConditionalDrawer : ExtendedDrawer
    {
        protected bool InvokeMethod(SerializedProperty property)
        {
            var method = FindMethod(property);

            if (method.IsStatic)
                return (bool)method.Invoke(null, Array.Empty<object>());

            return (bool)method.Invoke(property.serializedObject.targetObject, Array.Empty<object>());
        }

        protected MethodInfo FindMethod(SerializedProperty property)
        {
            var myAttribute = (HideAttribute)attribute;
            if (string.IsNullOrWhiteSpace(myAttribute.ConditionMethod))
                return null;
            
            var type = myAttribute.Type ?? property.serializedObject.targetObject.GetType();
            var method = type.GetMethod(myAttribute.ConditionMethod, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (method == null)
                throw new InvalidOperationException("Method '" + myAttribute.ConditionMethod + "' not found in '" + type.FullName + "'");
            if (method.ReturnType != typeof(bool))
                throw new InvalidOperationException("Method '" + myAttribute.ConditionMethod + "' in '" + type.FullName + "' does not returns bool");
            if (method.GetParameters().Length > 0)
                throw new InvalidOperationException("Method '" + myAttribute.ConditionMethod + "' in '" + type.FullName + "' must have no parameters");

            return method;
        }
    }
}