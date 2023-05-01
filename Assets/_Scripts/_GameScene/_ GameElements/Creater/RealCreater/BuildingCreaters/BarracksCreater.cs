using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.UtilityCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.GameObjectUtility;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene._UI.Features;
using _Scripts._GameScene.GameObjectPools;
using _Scripts._GameScene.ManagersInGame;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters
{
    public class BarracksCreater : Abs_BuildingCreater
    {
        private SpawnPositionsCreater spawnPositionsCreater;
        

        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell)
        {
            Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
            
                if (barracks !=null)
                {
                    BarracksPool.SharedInstance.RemoveFromPoolList(barracks);
                    barracks.gameObject.SetActive(true);
                    barracks.transform.position = spawnPositionByPoint;
                    barracks.transform.rotation = Quaternion.identity;
                    Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                    Vector2Int startSpawnPositionByCell = startPositionByCell;
                    Vector2Int endSpawnPositionByCell = new Vector2Int(endPositionByCell.x, endPositionByCell.y);
                    List<Vector2Int> spawnPositionListByCell = FindSpawnPositionsByCell(startSpawnPositionByCell, endSpawnPositionByCell);
                    List<SpawnPosition> SpawnPositionList = TriggerSpawnPositionCreater(barracks.SpawnPositionHolder, barracks.SpawnPositionSizeByCell, spawnPositionListByCell);
                    barracks.InIt(startPositionByCell, endPositionByCell, SpawnPositionList);
                    return barracks;
                }
                else
                {
                    Debug.LogWarning("Empty BarrackPool");
                }

                return new Barracks();
            

            

        }



        private List<SpawnPosition> TriggerSpawnPositionCreater(Transform SpawnPositonStore, Vector2Int spawnPositonSizeByCell, List<Vector2Int> spawnCellPositionByCellList)
        {
            spawnPositionsCreater = new SpawnPositionsCreater();
            List<SpawnPosition> spawnPositionList = new List<SpawnPosition>();
            foreach (var spawnCellPositionByCell in spawnCellPositionByCellList)
            {
                Vector3 spawnPositionByPoint = GameSpace.SpawnPointFinder(GameSpace.ConvertCellToPoint(spawnCellPositionByCell), spawnPositonSizeByCell);
                SpawnPosition spawnPosition = spawnPositionsCreater.FactoryMethod(spawnPositionByPoint, spawnCellPositionByCell, spawnPositonSizeByCell);
                spawnPosition.transform.SetParent(SpawnPositonStore);
                spawnPosition.gameObject.SetActive(true);
                spawnPositionList.Add(spawnPosition);
            }

            return spawnPositionList;
        }

        private List<Vector2Int> FindSpawnPositionsByCell(Vector2Int startPositionByCell, Vector2Int endPositionByCell)
        {
            List<Vector2Int> spawnPositionList = new List<Vector2Int>();
            Vector2Int spawnStartPositionByCell = new Vector2Int(startPositionByCell.x - 1, startPositionByCell.y - 1);
            Vector2Int spawnEndPositionByCell = new Vector2Int(endPositionByCell.x + 1, endPositionByCell.y + 1);

            for (int i = spawnStartPositionByCell.x; i < spawnEndPositionByCell.x; i++)
            {
                
                Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnStartPositionByCell.y);
                spawnPositionList.Add(newSpawnPositionByCell);
            }

            for (int i = spawnStartPositionByCell.y + 1; i <= spawnEndPositionByCell.y; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(spawnStartPositionByCell.x, i);
                spawnPositionList.Add(newSpawnPositionByCell);
            }

            for (int i = spawnStartPositionByCell.y; i < spawnEndPositionByCell.y; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(spawnEndPositionByCell.x, i);
                spawnPositionList.Add(newSpawnPositionByCell);
            }

            for (int i = spawnStartPositionByCell.x + 1; i <= spawnEndPositionByCell.x; i++)
            {
                Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnEndPositionByCell.y);
                spawnPositionList.Add(newSpawnPositionByCell);
            }
            
            return spawnPositionList;

        }

        
        
        
    }
}
