using System;
using System.Collections.Generic;
using _Scripts.Data.Enums;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine;

namespace _Scripts.Data.ScriptableObjects
{
   [CreateAssetMenu(fileName = "SoldierTypes", menuName = "ScriptableObjects/SodierType", order = 1)]
   public class SoldierTypeData : ScriptableObject
   {
      [SerializeField] public List<SoldierData> _soldierTypeList;

   
   
      public SoldierTypeData()
      {
         _soldierTypeList = new List<SoldierData>();
         EnumSoldierTyper[] typers = (EnumSoldierTyper[])EnumSoldierTyper.GetValues(typeof(EnumSoldierTyper));
         foreach (var variableTyper in typers)
         {
            _soldierTypeList.Add(new SoldierData(variableTyper));
         }
      
     
      
      }

      [Serializable]
      public class SoldierData
      {
         [SerializeField,ReadValueLockValue] private EnumSoldierTyper _type;
         [SerializeField] private int _damage = default(int);
         [SerializeField] private int _health = default(int); 
       
         [field:Serializable]public EnumSoldierTyper Type
         {
            get => _type;
         }
         [field:Serializable] public int Damage
         {
            get => _damage;
         }
         [field:Serializable] public int Health
         {
            get => _health;
         }

         public SoldierData(EnumSoldierTyper type)
         {
            _type = type;


         }
      }
  
   
   }
}
