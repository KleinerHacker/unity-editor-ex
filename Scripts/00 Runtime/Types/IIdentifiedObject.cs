using System;

namespace ExtendedEditor.Runtime.editor_ex.Scripts._00_Runtime.Types
{
    public interface IIdentifiedObject<out T> where T : Enum
    {
        T Identifier { get; }
    }
}