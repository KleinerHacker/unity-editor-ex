using UnityEditor;
using UnityEngine;

namespace UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor.Utils
{
    public static class AssetDatabaseEx
    {
        public static GUID GetGUID(Object obj)
        {
            var assetPath = AssetDatabase.GetAssetPath(obj);
            return AssetDatabase.GUIDFromAssetPath(assetPath);
        }
        
        public static GUID GetGUID(int instanceId)
        {
            var assetPath = AssetDatabase.GetAssetPath(instanceId);
            return AssetDatabase.GUIDFromAssetPath(assetPath);
        }
    }
}