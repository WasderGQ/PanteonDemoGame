using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.GameObjectUtility;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.UtilityCreaters
{
    public class SpawnPositionsCreater : AttachmentCreater<SpawnPosition>
    {
        public SpawnPosition FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell,Vector2Int prodcutSizeByCell)
        {
            SpawnPosition spawnPosition = SpawnPositionPool.SharedInstance.GetPooledObject();
            if (spawnPosition != null)
            {
                SpawnPositionPool.SharedInstance.RemoveFromPoolList(spawnPosition);
                spawnPosition.transform.position = spawnPositionByPoint;
                spawnPosition.transform.rotation = Quaternion.identity;
                spawnPosition.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + prodcutSizeByCell.x - 1, startPositionByCell.y + prodcutSizeByCell.y - 1);
                spawnPosition.InIt(startPositionByCell);
                return spawnPosition;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new SpawnPosition();
            }
            
            
            
            
            
        }

    
    }
}

