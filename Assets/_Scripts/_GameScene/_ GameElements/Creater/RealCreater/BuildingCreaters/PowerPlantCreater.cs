using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters
{
    public class PowerPlantCreater : Abs_BuildingCreater
    {
        public override IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell)
        {
            if (PowerPlantPool.SharedInstance.PoolChecker())
            {
                PowerPlant powerPlant = PowerPlantPool.SharedInstance.GetPooledObject();
                PowerPlantPool.SharedInstance.RemoveFromPoolList(powerPlant);
                powerPlant.transform.position = spawnPositionByPoint;
                powerPlant.transform.rotation = Quaternion.identity;
                powerPlant.gameObject.SetActive(true);
                Vector2Int endPositionByCell = new Vector2Int(startPositionByCell.x + PowerPlant.GameObjectSizeByCell.x - 1, startPositionByCell.y + PowerPlant.GameObjectSizeByCell.y - 1);
                powerPlant.InIt(startPositionByCell, endPositionByCell);

                return powerPlant;
            }
            else
            {
                Debug.LogWarning("Empty BarrackPool");
                return new Barracks();
            } 
        }

    }
}
