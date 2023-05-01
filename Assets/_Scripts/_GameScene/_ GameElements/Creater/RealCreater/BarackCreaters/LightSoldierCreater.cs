using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._PlayerControl;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class LightSoldierCreater : Abs_SoldierCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell,Vector2Int productSizeByCell)
        {
            if (LightSoldierPool.SharedInstance.PoolChecker())
            {
                LightSoldier lightSoldier = LightSoldierPool.SharedInstance.GetPooledObject();
                if (lightSoldier != null)
                {
                    LightSoldierPool.SharedInstance.RemoveFromPoolList(lightSoldier);
                    lightSoldier.transform.position = spawnPositionByPoint;
                    lightSoldier.transform.rotation = Quaternion.identity;
                    lightSoldier.gameObject.SetActive(true);
                    Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                    lightSoldier.InIt(startPositionByCell, endPositionByCell);
                    lightSoldier.GetComponent<SoldierController>().InIt();
                    return lightSoldier;
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
