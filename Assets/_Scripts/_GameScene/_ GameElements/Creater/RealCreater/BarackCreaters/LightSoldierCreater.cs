using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class LightSoldierCreater : SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (LightSoldierPool.SharedInstance.PoolChecker())
            {
                LightSoldier lightSoldier = LightSoldierPool.SharedInstance.GetPooledObject();
                LightSoldierPool.SharedInstance.RemoveFromPoolList(lightSoldier);
                lightSoldier.transform.position = spawnPositionByPoint;
                lightSoldier.transform.rotation = Quaternion.identity;
                lightSoldier.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                lightSoldier.InIt(startPositionByCell, endPositionByCell);

                return lightSoldier;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            }
        }
    }
}
