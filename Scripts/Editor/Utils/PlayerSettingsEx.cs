using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditor.Build;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils
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