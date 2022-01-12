using System;
using UnityEditor;

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

                    (leftProperty.managedReferenceValue, rightProperty.managedReferenceValue) =
                        (rightProperty.managedReferenceValue, leftProperty.managedReferenceValue);
                }
            } while (left < right);
            
            // Tausche Pivotelement ([rightIn]) mit neuer endgültiger Position ([left])
            if (comparison(property.GetArrayElementAtIndex(left), pivot) > 0)
            {
                var leftProperty = property.GetArrayElementAtIndex(left);
                var rightProperty = property.GetArrayElementAtIndex(rightIn);

                (leftProperty.managedReferenceValue, rightProperty.managedReferenceValue) =
                    (rightProperty.managedReferenceValue, leftProperty.managedReferenceValue);
            }

            return left; // gib die Position des Pivotelements zurück
        }
    }
}