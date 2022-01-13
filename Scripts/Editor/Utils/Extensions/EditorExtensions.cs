using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions
{
    public static class EditorExtensions
    {
        public static SerializedProperty[] FindPropertiesRelative(this SerializedProperty property, string name)
        {
            var relativeSize = property.FindPropertyRelative(name + ".Array.size").intValue;
            var relativeProperties = new SerializedProperty[relativeSize];
            for (var i = 0; i < relativeSize; i++)
            {
                relativeProperties[i] = property.FindPropertyRelative(name + ".Array.data[" + i + "]");
            }

            return relativeProperties;
        }

        public static SerializedProperty[] FindProperties(this SerializedObject o, string name)
        {
            var size = o.FindProperty(name + ".Array.size").intValue;
            var properties = new SerializedProperty[size];
            for (var i = 0; i < size; i++)
            {
                properties[i] = o.FindProperty(name + ".Array.data[" + i + "]");
            }

            return properties;
        }

        public static void SortArray(this SerializedProperty property, Comparison<SerializedProperty> comparison) =>
            QuicksortArray(0, property.arraySize - 1, property, comparison);

        private static void QuicksortArray(int left, int right, SerializedProperty property, Comparison<SerializedProperty> comparison)
        {
            if (left < right)
            {
                var splitter = Split(left, right, property, comparison);
                QuicksortArray(left, splitter - 1, property, comparison);
                QuicksortArray(splitter + 1, right, property, comparison);
            }
        }

        private static int Split(int leftIn, int rightIn, SerializedProperty property, Comparison<SerializedProperty> comparison)
        {
            var left = leftIn;
            //Starte mit j links vom Pivotelement
            var right = rightIn - 1;
            var pivot = property.GetArrayElementAtIndex(rightIn);

            do
            {
                //Suche von links ein Element, welches größer als das Pivotelement ist
                while (comparison(property.GetArrayElementAtIndex(left), pivot) < 0 && left < rightIn)
                    left += 1;

                //Suche von rechts ein Element, welches kleiner als das Pivotelement ist
                while (comparison(property.GetArrayElementAtIndex(right), pivot) >= 0 && right > leftIn)
                    right -= 1;

                if (left < right)
                {
                    var leftProperty = property.GetArrayElementAtIndex(left);
                    var rightProperty = property.GetArrayElementAtIndex(right);

                    var leftVal = leftProperty.GetValue();
                    var rightVal = rightProperty.GetValue();

                    leftProperty.SetValue(rightVal);
                    rightProperty.SetValue(leftVal);
                }
            } while (left < right);

            // Tausche Pivotelement ([rightIn]) mit neuer endgültiger Position ([left])
            if (comparison(property.GetArrayElementAtIndex(left), pivot) > 0)
            {
                var leftProperty = property.GetArrayElementAtIndex(left);
                var rightProperty = property.GetArrayElementAtIndex(rightIn);

                var leftVal = leftProperty.GetValue();
                var rightVal = rightProperty.GetValue();

                leftProperty.SetValue(rightVal);
                rightProperty.SetValue(leftVal);
            }

            return left; // gib die Position des Pivotelements zurück
        }

        /// (Extension) Get the value of the serialized property.
        public static object GetValue(this SerializedProperty property)
        {
            string propertyPath = property.propertyPath;
            object value = property.serializedObject.targetObject;
            int i = 0;
            while (NextPathComponent(propertyPath, ref i, out var token))
                value = GetPathComponentValue(value, token);
            return value;
        }

        /// (Extension) Set the value of the serialized property.
        public static void SetValue(this SerializedProperty property, object value)
        {
            Undo.RecordObject(property.serializedObject.targetObject, $"Set {property.name}");

            SetValueNoRecord(property, value);

            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.ApplyModifiedProperties();
        }

        /// (Extension) Set the value of the serialized property, but do not record the change.
        /// The change will not be persisted unless you call SetDirty and ApplyModifiedProperties.
        public static void SetValueNoRecord(this SerializedProperty property, object value)
        {
            string propertyPath = property.propertyPath;
            object container = property.serializedObject.targetObject;

            int i = 0;
            NextPathComponent(propertyPath, ref i, out var deferredToken);
            while (NextPathComponent(propertyPath, ref i, out var token))
            {
                container = GetPathComponentValue(container, deferredToken);
                deferredToken = token;
            }

            Debug.Assert(!container.GetType().IsValueType, $"Cannot use SerializedObject.SetValue on a struct object, as the result will be set on a temporary.  Either change {container.GetType().Name} to a class, or use SetValue with a parent member.");
            SetPathComponentValue(container, deferredToken, value);
        }

        // Union type representing either a property name or array element index.  The element
        // index is valid only if propertyName is null.
        private struct PropertyPathComponent
        {
            public string propertyName;
            public int elementIndex;
        }

        private static readonly Regex ArrayElementRegex = new Regex(@"\GArray\.data\[(\d+)\]", RegexOptions.Compiled);

        // Parse the next path component from a SerializedProperty.propertyPath.  For simple field/property access,
        // this is just tokenizing on '.' and returning each field/property name.  Array/list access is via
        // the pseudo-property "Array.data[N]", so this method parses that and returns just the array/list index N.
        //
        // Call this method repeatedly to access all path components.  For example:
        //
        //      string propertyPath = "quests.Array.data[0].goal";
        //      int i = 0;
        //      NextPropertyPathToken(propertyPath, ref i, out var component);
        //          => component = { propertyName = "quests" };
        //      NextPropertyPathToken(propertyPath, ref i, out var component) 
        //          => component = { elementIndex = 0 };
        //      NextPropertyPathToken(propertyPath, ref i, out var component) 
        //          => component = { propertyName = "goal" };
        //      NextPropertyPathToken(propertyPath, ref i, out var component) 
        //          => returns false
        private static bool NextPathComponent(string propertyPath, ref int index, out PropertyPathComponent component)
        {
            component = new PropertyPathComponent();

            if (index >= propertyPath.Length)
                return false;

            var arrayElementMatch = ArrayElementRegex.Match(propertyPath, index);
            if (arrayElementMatch.Success)
            {
                index += arrayElementMatch.Length + 1; // Skip past next '.'
                component.elementIndex = int.Parse(arrayElementMatch.Groups[1].Value);
                return true;
            }

            int dot = propertyPath.IndexOf('.', index);
            if (dot == -1)
            {
                component.propertyName = propertyPath.Substring(index);
                index = propertyPath.Length;
            }
            else
            {
                component.propertyName = propertyPath.Substring(index, dot - index);
                index = dot + 1; // Skip past next '.'
            }

            return true;
        }

        private static object GetPathComponentValue(object container, PropertyPathComponent component)
        {
            if (component.propertyName == null)
                return ((IList)container)[component.elementIndex];
            else
                return GetMemberValue(container, component.propertyName);
        }

        private static void SetPathComponentValue(object container, PropertyPathComponent component, object value)
        {
            if (component.propertyName == null)
                ((IList)container)[component.elementIndex] = value;
            else
                SetMemberValue(container, component.propertyName, value);
        }

        private static object GetMemberValue(object container, string name)
        {
            if (container == null)
                return null;
            var type = container.GetType();
            var members = type.GetMember(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < members.Length; ++i)
            {
                if (members[i] is FieldInfo field)
                    return field.GetValue(container);
                else if (members[i] is PropertyInfo property)
                    return property.GetValue(container);
            }

            return null;
        }

        private static void SetMemberValue(object container, string name, object value)
        {
            var type = container.GetType();
            var members = type.GetMember(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < members.Length; ++i)
            {
                if (members[i] is FieldInfo field)
                {
                    field.SetValue(container, value);
                    return;
                }
                else if (members[i] is PropertyInfo property)
                {
                    property.SetValue(container, value);
                    return;
                }
            }

            Debug.Assert(false, $"Failed to set member {container}.{name} via reflection");
        }
    }
}