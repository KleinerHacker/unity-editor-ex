using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Types;

namespace UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Utils
{
    public static class IdentifiableArrayUtils
    {
        public static T[] CreateIdentifiableArray<T,TI>(params TI[] excludes) where T : IIdentifiableObject<TI>
        {
            var sceneStates = Enum.GetValues(typeof(TI)).Cast<TI>().Distinct().ToArray();
            var list = new List<T>();

            foreach (var state in sceneStates)
            {
                if (excludes.Contains(state))
                    continue;
                
                list.Add((T) typeof(T).GetConstructor(new []{typeof(TI)}).Invoke(new object[] {state}));
            }

            return list.ToArray();
        }
        
        public static T[] UpdateIdentifiableArray<T,TI>(params T[] existing) where T : IIdentifiableObject<TI>
        {
            var sceneStates = Enum.GetValues(typeof(TI)).Cast<TI>().Distinct().ToArray();
            var list = new List<T>();

            foreach (var state in sceneStates)
            {
                var element = existing.FirstOrDefault(x => Equals(x.Identifier, state));
                if (element != null)
                {
                    list.Add(element);
                }
                else
                {
                    list.Add((T) typeof(T).GetConstructor(new[] {typeof(TI)}).Invoke(new object[] {state}));
                }
            }

            return list.ToArray();
        }
    }
}