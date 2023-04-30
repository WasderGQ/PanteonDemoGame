using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class HeavySoldierCreater : SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (HeavySoldierPool.SharedInstance.PoolChecker())
            {
                HeavySoldier heavySoldier = HeavySoldierPool.SharedInstance.GetPooledObject();
                if (heavySoldier != null)
                {
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
                    Debug.LogWarning("HeavySoldier Pool Empty");
                   
                }

            }
            return null;
        }
    }
}
