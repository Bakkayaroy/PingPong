using System;
using ModestTree;
using UnityEditor;
using UnityEngine;

namespace Internal.Scripts.ScriptableObjects
{
    public class ScriptableObjectIdAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    public class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif

    public class IdentifiedScriptableObject : ScriptableObject
    {
        public Guid Id;

        [ScriptableObjectId] 
        [SerializeField] private string _id = string.Empty;
        
        private void Awake()
        {
            Id = Guid.TryParse(_id, out var id) ? id : Guid.Empty;
        }

        public void OnValidate()
        {
            if (Id == Guid.Empty)
            {
                if (_id.IsEmpty())
                {
                    ResetId();
                }
                else
                {
                    Id = Guid.Parse(_id);
                }
            } 
        }

        [ContextMenu("ResetId")]
        private void ResetId()
        {
            Id = Guid.NewGuid();
            _id = Id.ToString();
        }

        public void Reset()
        {
            ResetId();
        }
        
    }
}