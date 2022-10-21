using System;
using UnityEngine;

namespace UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra
{
    public abstract class ConditionalAttribute : PropertyAttribute
    {
        public string ConditionMethod { get; set; }
        public Type Type { get; set; }
    }
}