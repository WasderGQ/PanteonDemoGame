using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Creater.VirtualCreater.PowerPlantCreaters;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.__GameElements.Products.VirtualProduct;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant>, IRealProduct, IVulnerable
    {
        #region Data

        [SerializeField] private BuildingTypeData _buildingTypeData;

        #endregion
        
        
        #region Public Property
        
        #region Statics

        

        #endregion
        
        #region Regular

        public List<IRealProduct> ProductList { get => _electric; }
        public Vector2Int StartPositionByCell { get => _startPositionByCell; }
        public Vector2Int EndPositionByCell { get => _endPositionByCell; }
        public Transform MyTransform { get => transform; }

        #endregion
        
        #endregion

        #region Private Variable
        
        private List<IRealProduct> _electric;
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        
        #endregion

        #region OnStart Func.

        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            SetVariable();
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
            
            
        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }
        

        private void SetVariable()
        {
            _electric = new List<IRealProduct>();
            _maxHealth = _buildingTypeData._buildingTypeList[(int)EnumBuildingType.PowerPlant].Health;
            _currentHealth = _maxHealth;
            
        }
        

        #endregion
        
        
        private int _maxHealth;
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
        }
        public UnityEvent<IAttacker> EventTakeDamage { get; }
        public void TakeDamage(IAttacker attacker)
        {
            _currentHealth = _currentHealth - attacker.Damage;
            CheckAmIDead();
        }
        private void  CheckAmIDead()
        {
            if(_currentHealth <= 0)
            {
                transform.SetParent(PowerPlantPool.SharedInstance.ParentPoolObject);
                PowerPlantPool.SharedInstance.PooledObjects.Add(this);
                gameObject.SetActive(false);
                
            }
        }
       
        
        


      
    }

}