using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.GameObjectUtility;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters
{
    public class BarracksCreater : BuildingCreater
    {


        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (BarracksPool.SharedInstance.PoolChecker())
            {
                Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
                BarracksPool.SharedInstance.RemoveFromPoolList(barracks);
                barracks.transform.position = spawnPositionByPoint;
                barracks.transform.rotation = Quaternion.identity;
                barracks.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                List<IRealProduct> spawnPositionList = CreateBarracksSpawnPoints(startPositionByCell, endPositionByCell);
                barracks.InIt(startPositionByCell, endPositionByCell, spawnPositionList);

                return barracks;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }
        }

        private List<IRealProduct> CreateBarracksSpawnPoints(Vector2Int startPositionByCell, Vector2Int endPositionByCell)
        {
            List<IRealProduct> spawnPositionList = new List<IRealProduct>();
            Vector2Int spawnStartPositionByCell = new Vector2Int(startPositionByCell.x - 1, startPositionByCell.y - 1);
            Vector2Int spawnEndPositionByCell = new Vector2Int(endPositionByCell.x + 1, endPositionByCell.y + 1);

            for (int i = spawnStartPositionByCell.x; i < spawnEndPositionByCell.x; i++)
            {
                if (i >= GameSpace.GameSpaceStartAreaByCell.x && i <= GameSpace.GameSpaceEndAreaByCell.x && spawnStartPositionByCell.y >= GameSpace.GameSpaceStartAreaByCell.y)
                {
                    spawnPositionList.Add(new SpawnPosition(new Vector2Int(i, spawnStartPositionByCell.y)));
                }
            }
         
            for (int i = spawnStartPositionByCell.y+1; i <= spawnEndPositionByCell.y; i++)
            {
                if (i >= GameSpace.GameSpaceStartAreaByCell.y && i <= GameSpace.GameSpaceEndAreaByCell.y && spawnStartPositionByCell.x >= GameSpace.GameSpaceStartAreaByCell.x)
                {
                    spawnPositionList.Add(new SpawnPosition(new Vector2Int(spawnStartPositionByCell.x, i)));
                }
            }
         
            for (int i = spawnStartPositionByCell.y ; i < spawnEndPositionByCell.y; i++)
            {
                if (i >= GameSpace.GameSpaceStartAreaByCell.y && i <= GameSpace.GameSpaceEndAreaByCell.y && spawnEndPositionByCell.x <= GameSpace.GameSpaceEndAreaByCell.x)
                {
                    spawnPositionList.Add(new SpawnPosition(new Vector2Int(spawnEndPositionByCell.x, i)));
                }
            }

            for (int i = spawnStartPositionByCell.x + 1; i <= spawnEndPositionByCell.x; i++)
            {
                if (i >= GameSpace.GameSpaceStartAreaByCell.x && i <= GameSpace.GameSpaceEndAreaByCell.x && spawnEndPositionByCell.y <= GameSpace.GameSpaceEndAreaByCell.y)
                {
                    spawnPositionList.Add(new SpawnPosition(new Vector2Int(i, spawnEndPositionByCell.y)));
                }
            }

            return spawnPositionList;

        }
    }






}

