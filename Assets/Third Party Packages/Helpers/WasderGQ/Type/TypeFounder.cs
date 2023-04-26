using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TypeFounder 
{
    private int HowMuchProductTypeYouHave<T>(object SearchObject)//Every ICreater creating one product type.
    {
        Type factorType = SearchObject.GetType();
        int variableCount = 0;

        foreach (FieldInfo field in factorType.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            if (field.FieldType == typeof(T))
            {
                variableCount++;
            }
        }

        return variableCount;
    }
}
