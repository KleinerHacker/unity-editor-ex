using UnityEditor;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils
{
    public static class AssetBundleUtility
    {
        public static void CreateAssetBundle(string name)
        {
            var assetImporter = AssetImporter.GetAtPath("Assets");
            var oldName = assetImporter.assetBundleName;

            assetImporter.assetBundleName = name;
            assetImporter.SaveAndReimport();

            assetImporter.assetBundleName = oldName;
            assetImporter.SaveAndReimport();
        }

        public static bool HasBundle(Object o)
        {
            var assetPath = AssetDatabase.GetAssetPath(o);
            var assetImporter = AssetImporter.GetAtPath(assetPath);

            return !string.IsNullOrEmpty(assetImporter.assetBundleName);
        }
    }
}