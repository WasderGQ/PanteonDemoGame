using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._PlayerControl;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class MedimuSoldierCreater : Abs_SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell)
        {
            if (MediumSoldierPool.SharedInstance.PoolChecker())
            {
                MediumSoldier mediumSoldier = MediumSoldierPool.SharedInstance.GetPooledObject();
                if (mediumSoldier != null)
                {
                    MediumSoldierPool.SharedInstance.RemoveFromPoolList(mediumSoldier);
                    mediumSoldier.transform.position = spawnPositionByPoint;
                    mediumSoldier.transform.rotation = Quaternion.identity;
                    mediumSoldier.gameObject.SetActive(true);
                    Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + Barracks.GameObjectSizeByCell.x - 1, startPositionByCell.y + Barracks.GameObjectSizeByCell.y - 1);
                    mediumSoldier.InIt(startPositionByCell, endPositionByCell);
                    mediumSoldier.GetComponent<SoldierController>().InIt();
                    return mediumSoldier;
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
