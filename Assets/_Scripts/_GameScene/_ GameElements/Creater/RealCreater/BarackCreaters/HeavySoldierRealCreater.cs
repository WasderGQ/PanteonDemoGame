using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public class HeavySoldierCreater : SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell)
        {
            if (BarracksPool.SharedInstance.PoolChecker())
            {
                HeavySoldier heavySoldier = HeavySoldierPool.SharedInstance.GetPooledObject();
                HeavySoldierPool.SharedInstance.RemoveFromPoolList(heavySoldier);
                heavySoldier.transform.position = spawnPositionByPoint;
                heavySoldier.transform.rotation = Quaternion.identity;
                heavySoldier.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                heavySoldier.InIt(startPositionByCell, endPositionByCell);

                return heavySoldier;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }
        }
    }
}
