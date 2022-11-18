using System;
using UnityEditor;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor.Windows
{
    internal sealed class InputWindow : EditorWindow
    {
        #region Properties

        public string Message { get; set; }
        public string Text { get; set; }
        public string OkButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public bool? ResultState { get; private set; }

        #endregion

        private void OnEnable()
        {
            minSize = new Vector2(300f, 75f);
            maxSize = minSize;
        }

        private void OnGUI()
        {
            GUILayout.Label(Message);
            Text = GUILayout.TextField(Text);
            
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(OkButtonText))
                {
                    ResultState = true;
                    Close();
                }

                if (GUILayout.Button(CancelButtonText))
                {
                    ResultState = false;
                    Close();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}