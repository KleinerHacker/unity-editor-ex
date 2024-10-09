using System;
using System.Linq;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor.Utils;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor.Extra
{
    [CustomPropertyDrawer(typeof(InputDeviceAttribute))]
    public sealed class InputDeviceDrawer : ExtendedDrawer
    {
        private static readonly (string, string)[] InputDevices = InputDeviceUtils.FindSupportedInputDeviceTypes();
        private static readonly string[] InputDeviceNames = InputDevices.Select(x => x.Item1).ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var index = string.IsNullOrWhiteSpace(property.stringValue) ? -1 : InputDevices.IndexOf(x => string.Equals(x.Item2, property.stringValue, StringComparison.Ordinal));
            index = EditorGUI.Popup(position, index, InputDeviceNames);
            property.stringValue = index >= 0 ? InputDevices[index].Item2 : null;
        }
    }
}