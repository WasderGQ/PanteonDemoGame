using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BuildCreaters
{
    public class BarracksCreater : BuildCreater
    {
        public override IProduct FactoryCreate(Vector3 spawnPositionByPoint)
        {
            if (!IsBarracksPoolEmphty())
            {
                
                Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
                if (barracks != null)
                {
                    barracks.transform.position = spawnPositionByPoint;
                    barracks.transform.rotation = Quaternion.identity;
                    barracks.gameObject.SetActive(true);
                    Vector2Int barracksEndByCell = new Vector2Int(spawnPositionByPoint.x + Barracks.GameObjectSizeByCell.x, spawnPositionByPoint.y + Barracks.GameObjectSizeByCell.y);
                    barracks.InIt(spawnPositionByPoint,barracksEndByCell);
                }
            }
            else
            {
                Debug.LogWarning("Pool is Emphty. You cant spawn more Barracks");
            }
        }
        private bool IsBarracksPoolEmphty()
        { 
            List<Barracks> inActive = BarracksPool.SharedInstance.PooledObjects.FindAll(x => x.isActiveAndEnabled == true);
            if (BarracksPool.SharedInstance.PooledObjects.Count - inActive.Count == 0)
            {
                return true;
            }
            if (BarracksPool.SharedInstance.PooledObjects.Count == 0)
            {
                return true;
            }
            return false;
        }
    } 
}

