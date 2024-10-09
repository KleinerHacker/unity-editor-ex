using System.Linq;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditor.Build;

namespace UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor.Utils
{
    public static class PlayerSettingsEx
    {
        public static bool IsScriptingSymbolDefined(string symbol)
        {
            PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup), out var symbols);
            return symbols.Contains(symbol);
        }

        public static void AddScriptingSymbol(string symbol)
        {
            PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup), out var symbols);
            PlayerSettings.SetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                symbols.Append(symbol).ToArray()
            );
        }

        public static void RemoveScriptingSymbol(string symbol)
        {
            PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup), out var symbols);
            PlayerSettings.SetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                symbols.Remove(symbol).ToArray()
            );
        }
    }
}