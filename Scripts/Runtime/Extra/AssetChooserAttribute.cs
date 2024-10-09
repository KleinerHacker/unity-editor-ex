using System;
using UnityEngine;

namespace UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Extra
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetChooserAttribute : PropertyAttribute
    {
        public Type Type { get; }

        public AssetChooserAttribute(Type type)
        {
            Type = type;
        }
    }
}