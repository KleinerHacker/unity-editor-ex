using System;

namespace UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Types
{
    public interface IIdentifiableObject<out T>
    {
        T Identifier { get; }
    }
}