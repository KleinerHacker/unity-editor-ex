#if DEMO
using System;
using UnityEditorEx.Runtime.Projects.unity_editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityEditorEx.Demo.Projects.unity_editor_ex.Scripts.Demo.Extra
{
    [CreateAssetMenu(menuName = "Extra/Read Only/Demo Asset")]
    public sealed class DemoAsset : ScriptableObject
    {
        #region Inspector Data

        [ReadOnly]
        [SerializeField]
        private string uuid = Guid.NewGuid().ToString();
        
        [ReadOnly(ConditionMethod = "ReadOnlyCondition")]
        [SerializeField]
        private string opt = "Hello World!";

        [Space]
        [SerializeField]
        private bool optChange;

        #endregion

        public bool ReadOnlyCondition()
        {
            return optChange;
        }
    }
}
#endif