using System;
using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;
using UnityExtension.Editor.extension.Scripts.Editor.Utils;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Extra
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