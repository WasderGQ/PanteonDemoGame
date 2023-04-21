using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoldierTypeData))]
public class EditorManager : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SoldierTypeData myScriptableObject = (SoldierTypeData)target;
        if (myScriptableObject._soldierTypeList == null)
        {
            myScriptableObject._soldierTypeList = new List<SoldierTypeData.SoldierData>();
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_soldierTypeList"), true);

        serializedObject.ApplyModifiedProperties();
    }
    
    
    
    
    
    
}
        
