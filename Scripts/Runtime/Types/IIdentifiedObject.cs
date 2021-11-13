using System;

namespace UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Types
{
    public interface IIdentifiedObject<out T> where T : Enum
    {
        T Identifier { get; }
    }
}