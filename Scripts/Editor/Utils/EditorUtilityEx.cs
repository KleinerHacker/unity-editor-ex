using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Windows;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils
{
    public static class EditorUtilityEx
    {
        public static string DisplayInputDialog(string title, string message, string okButton, string cancelButton) =>
            DisplayInputDialog(title, message, "", okButton, cancelButton);

        public static string DisplayInputDialog(string title, string message, string text, string okButton, string cancelButton)
        {
            var window = ScriptableObject.CreateInstance<InputWindow>();
            window.titleContent = new GUIContent(title);
            window.Message = message;
            window.Text = text;
            window.OkButtonText = okButton;
            window.CancelButtonText = cancelButton;

            window.ShowModal();

            return window.ResultState.GetValueOrDefault(false) ? window.Text : null;
        }
    }
}