using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils
{
    public static class ExtendedEditorGUILayout
    {
        public static T AssetPopup<T>(Func<T, string> nameExtractor, SerializedProperty property, Action<T> onChanged = null) where T : Object
        {
            var assetNames = AssetDatabase.FindAssets("t:" + typeof(T).Name)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(path => (T)AssetDatabase.LoadAssetAtPath(path, typeof(T)))
                .Select(nameExtractor)
                .ToArray();

            var assetIndex = property.objectReferenceValue == null ? -1 : assetNames.ToList().IndexOf(nameExtractor((T) property.objectReferenceValue));
            var lastIndex = assetIndex;

            assetIndex = EditorGUILayout.Popup(assetIndex, assetNames);
            var newAsset = assetIndex < 0 || assetIndex >= assetNames.Length ? null : AssetDatabase.FindAssets("t:" + typeof(T).Name)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(path => (T) AssetDatabase.LoadAssetAtPath(path, typeof(T)))
                .FirstOrDefault(asset => nameExtractor(asset) == assetNames[assetIndex]);

            if (assetIndex != lastIndex)
            {
                Debug.Log("Setup Asset from Popup: " + typeof(T).Name + " / " + newAsset);
                property.objectReferenceValue = newAsset;
                
                onChanged?.Invoke(newAsset);
            }

            return newAsset;
        }

        public static void SymbolField(GUIContent label, string symbolName, GUIStyle guiStyle, params GUILayoutOption[] layoutOptions)
        {
            var defined = PlayerSettingsEx.IsScriptingSymbolDefined(symbolName);
            var newDefined = guiStyle == null ?
                EditorGUILayout.Toggle(label, defined, layoutOptions) :
                EditorGUILayout.Toggle(label, defined, guiStyle, layoutOptions);
            if (defined != newDefined)
            {
                if (newDefined)
                {
                    PlayerSettingsEx.AddScriptingSymbol(symbolName);
                }
                else
                {
                    PlayerSettingsEx.RemoveScriptingSymbol(symbolName);
                }
            }
        }

        public static void SymbolField(string label, string symbolName, GUIStyle guiStyle, params GUILayoutOption[] layoutOptions) =>
            SymbolField(new GUIContent(label), symbolName, guiStyle, layoutOptions);

        public static void SymbolField(GUIContent label, string symbolName, params GUILayoutOption[] layoutOptions) =>
            SymbolField(label, symbolName, null, layoutOptions);

        public static void SymbolField(string label, string symbolName, params GUILayoutOption[] layoutOptions) =>
            SymbolField(new GUIContent(label), symbolName, layoutOptions);
        
        public static void SymbolFieldLeft(GUIContent label, string symbolName, GUIStyle guiStyle, params GUILayoutOption[] layoutOptions)
        {
            var defined = PlayerSettingsEx.IsScriptingSymbolDefined(symbolName);
            var newDefined = guiStyle == null ? 
                EditorGUILayout.ToggleLeft(label, defined, layoutOptions) : 
                EditorGUILayout.ToggleLeft(label, defined, guiStyle, layoutOptions);
            if (defined != newDefined)
            {
                if (newDefined)
                {
                    PlayerSettingsEx.AddScriptingSymbol(symbolName);
                }
                else
                {
                    PlayerSettingsEx.RemoveScriptingSymbol(symbolName);
                }
            }
        }

        public static void SymbolFieldLeft(string label, string symbolName, GUIStyle guiStyle, params GUILayoutOption[] layoutOptions) =>
            SymbolFieldLeft(new GUIContent(label), symbolName, guiStyle, layoutOptions);

        public static void SymbolFieldLeft(GUIContent label, string symbolName, params GUILayoutOption[] layoutOptions) =>
            SymbolFieldLeft(label, symbolName, null, layoutOptions);

        public static void SymbolFieldLeft(string label, string symbolName, params GUILayoutOption[] layoutOptions) =>
            SymbolFieldLeft(new GUIContent(label), symbolName, layoutOptions);
    }
}