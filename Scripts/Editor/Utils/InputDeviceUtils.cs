using System;
using System.Linq;
using UnityEngine.InputSystem;

namespace UnityExtension.Editor.extension.Scripts.Editor.Utils
{
    internal static class InputDeviceUtils
    {
        public static (string,string)[] FindSupportedInputDeviceTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(InputDevice).IsAssignableFrom(x))
                .Select(x => (x.Name, x.AssemblyQualifiedName))
                .ToArray();
        }
    }
}