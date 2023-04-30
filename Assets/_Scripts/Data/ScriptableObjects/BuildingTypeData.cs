using System;
using System.Collections.Generic;
using _Scripts.Data.Enums;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine;

namespace _Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BuildingTypes", menuName = "ScriptableObjects/BuildingType", order = 2)]
    public class BuildingTypeData : ScriptableObject
    {
        [SerializeField] public List<BuildingData> _buildingTypeList;

   
   
        public BuildingTypeData()
        {
            _buildingTypeList = new List<BuildingData>();
            EnumBuildingType[] typers = (EnumBuildingType[])EnumBuildingType.GetValues(typeof(EnumSoldierTyper));
            foreach (var variableTyper in typers)
            {
                _buildingTypeList.Add(new BuildingData(variableTyper));
            }
      
     
      
        }

        [Serializable]
        public class BuildingData
        {
            [SerializeField,ReadValueLockValue] private EnumBuildingType _type;
            [SerializeField] private int _health = default(int); 
       
            [field:Serializable]public EnumBuildingType Type
            {
                get => _type;
            }
            [field:Serializable] public int Health
            {
                get => _health;
            }
            public BuildingData(EnumBuildingType type)
            {
                _type = type;


            }
        }
  
   
    }
    }