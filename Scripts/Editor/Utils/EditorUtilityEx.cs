using System;
using System.Reflection;
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

        public static void PlayAudio(AudioClip clip, int sample = 0, bool loop = false)
        {
            var unityEditorAssembly = typeof(AudioImporter).Assembly;
            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[]
                {
                    typeof(AudioClip),
                    typeof(int),
                    typeof(bool)
                },
                null
            );
            method.Invoke(null, new object[] { clip, sample, loop });
        }

        public static void StopAudio()
        {
            var unityEditorAssembly = typeof(AudioImporter).Assembly;
            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                "StopAllPreviewClips",
                BindingFlags.Static | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null
            );
            method.Invoke(null, Array.Empty<object>());
        }

        public static void SetLoop(bool loop)
        {
            var unityEditorAssembly = typeof(AudioImporter).Assembly;
            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                "LoopPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new Type[]
                {
                    typeof(bool)
                },
                null
            );
            method.Invoke(null, new object[] { loop });
        }

        public static bool IsAudioPlaying()
        {
            var unityEditorAssembly = typeof(AudioImporter).Assembly;
            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                "IsPreviewClipPlaying",
                BindingFlags.Static | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null
            );
            return (bool)method.Invoke(null, Array.Empty<object>());
        }

        public static int GetAudioSamplePosition()
        {
            var unityEditorAssembly = typeof(AudioImporter).Assembly;
            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                "GetPreviewClipSamplePosition",
                BindingFlags.Static | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null
            );
            return (int)method.Invoke(null, Array.Empty<object>());
        }

        public static bool ResetAllAudioClipPlayCountsOnPlay
        {
            get
            {
                var unityEditorAssembly = typeof(AudioImporter).Assembly;
                var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
                var property = audioUtilClass.GetProperty(
                    "resetAllAudioClipPlayCountsOnPlay",
                    BindingFlags.Static | BindingFlags.Public
                );
                return (bool) property.GetValue(null);
            }

            set
            {
                var unityEditorAssembly = typeof(AudioImporter).Assembly;
                var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
                var property = audioUtilClass.GetProperty(
                    "resetAllAudioClipPlayCountsOnPlay",
                    BindingFlags.Static | BindingFlags.Public
                );
                property.SetValue(null, value);
            }
        }
    }
}