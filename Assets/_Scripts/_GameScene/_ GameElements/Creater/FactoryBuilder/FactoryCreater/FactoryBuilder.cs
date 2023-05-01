using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.AttachmentCreaters;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Factorys;
using _Scripts._GameScene.__GameElements.Products.RealProduct.GameObjectUtility;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.FactoryBuilder.FactoryCreater
{
    public class FactoryBuilder : FactoryCreater2<Products.RealProduct.Factorys.Barracks,PowerPlant>
    {
        private SpawnPositionsCreater _spawnPositionsCreater;
        private List<Products.RealProduct.Factorys.Barracks> _barracksList;
        private List<PowerPlant> _powerPlantList;
        public FactoryBuilder()
        {
            _spawnPositionsCreater = new SpawnPositionsCreater();
            _barracksList = new List<Products.RealProduct.Factorys.Barracks>();
            _powerPlantList = new List<PowerPlant>();
        }
        
        
        
        
        
        public override List<Products.RealProduct.Factorys.Barracks> CreatedFactoryList1
        {
            get => _barracksList;
        }
        
        public override  Products.RealProduct.Factorys.Barracks FactoryMethod1(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell)
        {
            
            
            if (BarracksPool.SharedInstance.PoolChecker())
            {
                Products.RealProduct.Factorys.Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
                barracks.gameObject.SetActive(true);
                barracks.transform.position = spawnPositionByPoint;
                barracks.transform.rotation = Quaternion.identity;
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                Vector2Int startSpawnPositionByCell = new Vector2Int(startPositionByCell.x-1,startPositionByCell.y-1);
                Vector2Int endSpawnPositionByCell = new Vector2Int(endPositionByCell.x+1, endPositionByCell.y+1);
                barracks.InIt(startPositionByCell, endPositionByCell, CreateCreatablePositions(startSpawnPositionByCell,endSpawnPositionByCell,barracks.SpawnPositionSizeByCell));
                _barracksList.Add(barracks);
                return barracks;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
            }

            return new Products.RealProduct.Factorys.Barracks();

        }
       
        private List<SpawnPosition> CreateCreatablePositions(Vector2Int startPositionByCell, Vector2Int endPositionByCell,Vector2Int spawnPositionSizeByCell)
        {
            List<SpawnPosition> spawnPositionList = new List<SpawnPosition>();
            Vector2Int spawnStartPositionByCell = startPositionByCell;
            Vector2Int spawnEndPositionByCell = endPositionByCell;

            for (int i = spawnStartPositionByCell.x; i < spawnEndPositionByCell.x; i++)
            {
                
                Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnStartPositionByCell.y);
                Vector3 spawnPositionByPoint = GameSpace.SpawnPointFinder(GameSpace.ConvertCellToPoint(newSpawnPositionByCell),spawnPositionSizeByCell);
                spawnPositionList.Add(_spawnPositionsCreater.FactoryMethod(spawnPositionByPoint,newSpawnPositionByCell,spawnPositionSizeByCell));
            }

            for (int i = spawnStartPositionByCell.y + 1; i <= spawnEndPositionByCell.y; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(spawnStartPositionByCell.x, i);
                Vector3 spawnPositionByPoint = GameSpace.SpawnPointFinder(GameSpace.ConvertCellToPoint(newSpawnPositionByCell),spawnPositionSizeByCell);
                spawnPositionList.Add(_spawnPositionsCreater.FactoryMethod(spawnPositionByPoint,newSpawnPositionByCell,spawnPositionSizeByCell));
            }

            for (int i = spawnStartPositionByCell.y; i < spawnEndPositionByCell.y; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(spawnEndPositionByCell.x, i);
                Vector3 spawnPositionByPoint =GameSpace.SpawnPointFinder(GameSpace.ConvertCellToPoint(newSpawnPositionByCell),spawnPositionSizeByCell);
                spawnPositionList.Add(_spawnPositionsCreater.FactoryMethod(spawnPositionByPoint,newSpawnPositionByCell,spawnPositionSizeByCell));
            }

            for (int i = spawnStartPositionByCell.x + 1; i <= spawnEndPositionByCell.x; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnEndPositionByCell.y);
                Vector3 spawnPositionByPoint = GameSpace.SpawnPointFinder(GameSpace.ConvertCellToPoint(newSpawnPositionByCell),spawnPositionSizeByCell);
                spawnPositionList.Add(_spawnPositionsCreater.FactoryMethod(spawnPositionByPoint,newSpawnPositionByCell,spawnPositionSizeByCell));
            }
            
            return spawnPositionList;

        }
        
        
       
        public override  List<PowerPlant> CreatedFactoryList2 
        { get => _powerPlantList; }
        
        public override PowerPlant FactoryMethod2(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell)
        {
            
            if (PowerPlantPool.SharedInstance.PoolChecker())
            {
                PowerPlant powerPlant = PowerPlantPool.SharedInstance.GetPooledObject();
                powerPlant.transform.position = spawnPositionByPoint;
                powerPlant.transform.rotation = Quaternion.identity;
                powerPlant.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                powerPlant.InIt(startPositionByCell, endPositionByCell);
                CreatedFactoryList2.Add(powerPlant);
                return powerPlant;

            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new PowerPlant();
            }
        }

       
    }
}
