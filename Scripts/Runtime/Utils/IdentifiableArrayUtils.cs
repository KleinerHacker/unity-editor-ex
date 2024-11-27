using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Types;

namespace UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Utils
{
    public static class IdentifiableArrayUtils
    {
        public static T[] CreateIdentifiableArray<T,TI>(params TI[] excludes) where T : IIdentifiableObject<TI> where TI : Enum
        {
            var values = Enum.GetValues(typeof(TI)).Cast<TI>().Distinct().ToArray();
            var list = new List<T>();

            foreach (var value in values)
            {
                if (excludes.Contains(value))
                    continue;
                
                list.Add((T) typeof(T).GetConstructor(new []{typeof(TI)}).Invoke(new object[] {value}));
            }

            return list.ToArray();
        }
        
        public static T[] UpdateIdentifiableArray<T,TI>(params T[] existing) where T : IIdentifiableObject<TI> where TI : Enum
        {
            var values = Enum.GetValues(typeof(TI)).Cast<TI>().Distinct().ToArray();
            return UpdateIdentifiableArray<T, TI>(existing, values);
        }

        public static T[] UpdateIdentifiableArray<T, TI>(T[] existing, params TI[] values) where T : IIdentifiableObject<TI>
        {
            var list = new List<T>();

            foreach (var value in values)
            {
                var element = existing.FirstOrDefault(x => Equals(x.Identifier, value));
                if (element != null)
                {
                    list.Add(element);
                }
                else
                {
                    list.Add((T) typeof(T).GetConstructor(new[] {typeof(TI)}).Invoke(new object[] {value}));
                }
            }

            return list.ToArray();
        }
    }
}