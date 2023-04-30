using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ.CustomAttributes
{/*
    [System.Serializable]
    public class SerializableDictionary : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(SerializableDictionary))]
    public class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty dictionaryProperty = property.FindPropertyRelative("dictionary");
            SerializedProperty itemProperty = null;

            for (int i = 0; i < dictionaryProperty.arraySize; i++)
            {
                itemProperty = dictionaryProperty.GetArrayElementAtIndex(i);

                SerializedProperty keyProperty = itemProperty.FindPropertyRelative("key");
                SerializedProperty valueProperty = itemProperty.FindPropertyRelative("value");

                Rect keyRect = new Rect(position.x, position.y + i * (EditorGUIUtility.singleLineHeight + 2),
                    position.width / 2f - 5, EditorGUIUtility.singleLineHeight);
                Rect valueRect = new Rect(position.x + position.width / 2f + 5,
                    position.y + i * (EditorGUIUtility.singleLineHeight + 2),
                    position.width / 2f - 5, EditorGUIUtility.singleLineHeight);

                EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none);
                EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
            }

        }
    }*/
}