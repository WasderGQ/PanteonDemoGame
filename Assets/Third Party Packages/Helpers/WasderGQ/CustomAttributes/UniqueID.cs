using System;
using UnityEditor;
using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ.CustomAttributes
{
   
        public class UniqueID : PropertyAttribute
        {
            
        }

        [CustomPropertyDrawer(typeof(UniqueID))]
        public class UniqueIdDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                GUI.enabled = false;
                if (string.IsNullOrEmpty(property.stringValue))
                    property.stringValue = Guid.NewGuid().ToString("N");

                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
        }
    
}