using UnityEditor;
using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ.CustomAttributes
{
    
    public class ReadValueLockValue : PropertyAttribute
    {
    }
    /*
    
    [CustomPropertyDrawer(typeof(ReadValueLockValue))] 
    public class ReadValueLockValueEditor : PropertyDrawer 
    {
        public override void OnGUI(Rect location, SerializedProperty property, GUIContent line) 
        {
            GUI.enabled = false; 
            EditorGUI.PropertyField(location, property, line, true); 
            GUI.enabled = true; 
        }
    }

    */
}