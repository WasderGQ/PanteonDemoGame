using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class MediumSoldierCreater : SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (MediumSoldierPool.SharedInstance.PoolChecker())
            {
                MediumSoldier mediumSoldier = MediumSoldierPool.SharedInstance.GetPooledObject();
                MediumSoldierPool.SharedInstance.RemoveFromPoolList(mediumSoldier);
                mediumSoldier.transform.position = spawnPositionByPoint;
                mediumSoldier.transform.rotation = Quaternion.identity;
                mediumSoldier.gameObject.SetActive(true);
                Vector2Int endPositionByCell = startPositionByCell;
                mediumSoldier.InIt(startPositionByCell, endPositionByCell);

                return mediumSoldier;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }
        }
    }
}
