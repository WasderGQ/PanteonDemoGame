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
    public class PowerPlant : FactoryHave1Creater<PowerPlant>  ,IProduct
    {
        
        public Transform Transform { get => this.transform; }
        
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private Vector2Int _spawnStartPositionByCell;
        private Vector2Int _spawnEndPositionByCell;
        
        private List<Vector2Int> _spawnPointList;
        
        public UnityEvent CreateHeavySoldier;
        public UnityEvent CreateMediumSoldier;
        public UnityEvent CreateLightSoldier;
       
        private readonly static Vector2Int _staticGameObjectSizeByCell = new Vector2Int(4, 4);

        private List<IProduct> _electric;

        

        private ElectricCreater _electricCreater;

        


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
            CreateHeavySoldier.AddListener(TriggerHeavySoldierCreater);
           
        }

        private void TriggerHeavySoldierCreater()
        {
           IProduct product  =_electricCreater.FactoryMethod();
           _electric.Add(product);

        }
        

       
        
        
       

    }

}