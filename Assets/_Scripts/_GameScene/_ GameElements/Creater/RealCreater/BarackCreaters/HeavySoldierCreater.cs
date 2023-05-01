using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._PlayerControl;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public class HeavySoldierCreater : Abs_SoldierCreater
    {
        public override async Task<IRealProduct> FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell,Vector2Int productSizeByCell)
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
                    Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                    heavySoldier.InIt(startPositionByCell, endPositionByCell);
                    heavySoldier.GetComponent<SoldierController>().InIt();
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
