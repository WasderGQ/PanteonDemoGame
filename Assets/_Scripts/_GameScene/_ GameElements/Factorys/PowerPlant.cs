using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Creater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Creater.PowerPlantCreaters;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._GameArea;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant> ,IProduct
    {
       
        
        #region Public Propertys

        #region Regular 

        public Transform MyTransform { get => this.transform; } 
        public static Vector2Int GameObjectSizeByCell { get => _staticGameObjectSizeByCell; }
        public List<IProduct> BuildingProductList { get => _electric; }
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
        public List<IGameSpaceOccupanter> Occupanters { get; }

        #endregion
        
        #region Events

        public UnityEvent CreateHeavySoldier;
        public UnityEvent CreateMediumSoldier;
        public UnityEvent CreateLightSoldier;

        #endregion
        
        #endregion
        
        #region Private Variable
        
        #region Static
       
        private readonly static Vector2Int _staticGameObjectSizeByCell = new Vector2Int(4, 4);
       
        #endregion

        #region Regular

        private List<Vector2Int> _spawnPointList;
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private Vector2Int _spawnStartPositionByCell;
        private Vector2Int _spawnEndPositionByCell;
        private List<IProduct> _electric;
        private ElectricCreater _electricCreater;

        #endregion
        
        
        #endregion
        
        #region OnStart

        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell,Vector2Int spawnStartPositionByCell,Vector2Int spawnEndPositionByCell)
        {
            AddListener();
            OnStartSetSpawnPoints(spawnStartPositionByCell,spawnEndPositionByCell);
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPoints(Vector2Int spawnStartPositionByCell,Vector2Int spawnEndPositionByCell)
        {
            _spawnStartPositionByCell = spawnStartPositionByCell;
            _spawnEndPositionByCell = spawnEndPositionByCell;
        }
        
        private void AddListener()
        {
            CreateHeavySoldier.AddListener(TriggerElectricCreater);
           
        }

        #endregion

        #region Event Trigger Func.

        private void TriggerElectricCreater()
        {
            IProduct product  =_electricCreater.FactoryMethod();
            _electric.Add(product);

        }

        #endregion
        
        

       
        
        
       

    }

}