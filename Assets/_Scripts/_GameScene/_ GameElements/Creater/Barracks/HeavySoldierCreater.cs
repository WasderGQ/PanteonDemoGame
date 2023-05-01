using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.Barracks
{
    public  class HeavySoldierCreater : SoldierCreater<HeavySoldier>
    {
        private List<HeavySoldier> _createdSoldierList;
        public override List<HeavySoldier> CreatedSoldierList { get; }


        public HeavySoldierCreater()
        {
            _createdSoldierList = new List<HeavySoldier>();
        }
        
        public override HeavySoldier FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell,Vector2Int productSizeByCell)
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
                    //heavySoldier.GetComponent<SoldierController>().InIt();
                    _createdSoldierList.Add(heavySoldier);
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
