using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierTypes", menuName = "ScriptableObjects/SodierType", order = 1)]
public class SoldierTypeData : ScriptableObject
{
   [SerializeField] public List<SoldierData> _soldierTypeList;

   
   
   public SoldierTypeData()
   {
      _soldierTypeList = new List<SoldierData>();
      EnumSoldierTyper[] typers = (EnumSoldierTyper[])EnumSoldierTyper.GetValues(typeof(EnumSoldierTyper));
      foreach (var varıableTyper in typers)
      {
         Debug.Log("enum dönüyor");
         _soldierTypeList.Add(new SoldierData(varıableTyper));
      }
      
     
      
   }

   [Serializable]
   public class SoldierData
   {
      [SerializeField,ReadValueLockValue] private EnumSoldierTyper _type;
      [SerializeField] private int _damage = default(int);
      [SerializeField] private int _health = default(int); 
       
      [field:NonSerialized]public EnumSoldierTyper Type
      {
         get => _type;
      }
      [field:NonSerialized] public int Damage
      {
         get => _damage;
      }
      [field:NonSerialized] public int Health
      {
         get => _health;
      }

      public SoldierData(EnumSoldierTyper type)
      {
         _type = type;


      }
   }
  
   
}
