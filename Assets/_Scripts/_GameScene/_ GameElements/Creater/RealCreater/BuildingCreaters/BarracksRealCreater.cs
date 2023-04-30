using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BuildingCreaters
{
    public class BarracksCreater : BuildingCreater
    {


        public override IRealProduct FactoryCreate(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (BarracksPool.SharedInstance.PoolChecker())
            {
                Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
                BarracksPool.SharedInstance.RemoveFromPoolList(barracks);
                barracks.transform.position = spawnPositionByPoint;
                barracks.transform.rotation = Quaternion.identity;
                barracks.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                List<IRealProduct> spawnPositionList = CreateSpawnPositions(startPositionByCell, endPositionByCell);
                barracks.InIt(startPositionByCell, endPositionByCell, spawnPositionList);

                return barracks;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }

        }



        private List<IRealProduct> CreateSpawnPositions(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            List<IRealProduct> spawnPositionList = new List<IRealProduct>();
            for (int i = startPositionByCell.x - 1; i <= endPositionByCell.x + 1; i++)
            {
                if(startPositionByCell.y >= GameSpace.GameSpaceStartAreaByCell.y && i >= GameSpace.GameSpaceStartAreaByCell.x)
                spawnPositionList.Add(new SpawnPosition(new Vector2Int(i,startPositionByCell.y-1)));
            }
            for (int j = startPositionByCell.y-1; j <= endPositionByCell.y+1; j++)
            {
                if(j >= GameSpace.GameSpaceStartAreaByCell.y && startPositionByCell.x >= GameSpace.GameSpaceStartAreaByCell.x)
                spawnPositionList.Add(new SpawnPosition(new Vector2Int(startPositionByCell.x-1,j)));
            }
            for (int k = endPositionByCell.x+1; k > startPositionByCell.x-1; k--)
            {
                if(k <= GameSpace.GameSpaceEndAreaByCell.x && endPositionByCell.y <= GameSpace.GameSpaceEndAreaByCell.y)
                spawnPositionList.Add(new SpawnPosition(new Vector2Int(k,endPositionByCell.y+1)));
            }
            for (int l = endPositionByCell.y+1; l > startPositionByCell.y-1; l--)
            {
                if(l <= GameSpace.GameSpaceEndAreaByCell.y && endPositionByCell.x <= GameSpace.GameSpaceEndAreaByCell.x)
                spawnPositionList.Add(new SpawnPosition(new Vector2Int(endPositionByCell.x+1,l)));
            }

            return spawnPositionList;
        }

        
        
    } 
}

