using System;
using UnityEngine;

namespace ExtendedEditor.Runtime.editor_ex.Scripts._00_Runtime.Extra
{
    public class AssetChooserAttribute : PropertyAttribute
    {
        public Type Type { get; }

        public AssetChooserAttribute(Type type)
        {
            Type = type;
        }
    }
}