using System;

namespace UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Types
{
    public interface IIdentifiedObject<out T> where T : Enum
    {
        T Identifier { get; }
    }
}