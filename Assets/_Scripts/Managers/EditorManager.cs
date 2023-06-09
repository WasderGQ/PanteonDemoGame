using System.Collections.Generic;
using _Scripts._GameScene._UI;
using _Scripts.Data.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
#if UNITY_EDITOR
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
    
    
    
#endif

    
}
        
