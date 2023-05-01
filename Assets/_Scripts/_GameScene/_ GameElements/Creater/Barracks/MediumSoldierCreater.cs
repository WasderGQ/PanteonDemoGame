using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.Barracks
{
    public class MediumSoldierCreater : SoldierCreater<MediumSoldier>
    {
        private List<MediumSoldier> _createdSoldierList;
        public override List<MediumSoldier> CreatedSoldierList { get; }


        public MediumSoldierCreater()
        {
            _createdSoldierList = new List<MediumSoldier>();
        }
        public override MediumSoldier FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell,Vector2Int productSizeByCell)
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
                    Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + productSizeByCell.x - 1, startPositionByCell.y + productSizeByCell.y - 1);
                    mediumSoldier.InIt(startPositionByCell, endPositionByCell);
                   // mediumSoldier.GetComponent<SoldierController>().InIt();
                    _createdSoldierList.Add(mediumSoldier);
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
