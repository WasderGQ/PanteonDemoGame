using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.GameObjectUtility;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters
{
    public class BarracksCreater : Abs_BuildingCreater
    {


        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, UnityEngine.Vector2Int startPositionByCell)
        {
            if (BarracksPool.SharedInstance.PoolChecker())
            {
                Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
                BarracksPool.SharedInstance.RemoveFromPoolList(barracks);
                barracks.transform.position = spawnPositionByPoint;
                barracks.transform.rotation = Quaternion.identity;
                barracks.gameObject.SetActive(true);
                UnityEngine.Vector2Int endPositionByCell = new UnityEngine.Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                barracks.InIt(startPositionByCell, endPositionByCell);
                return barracks;

            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }
        }

    }
}
