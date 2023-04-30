using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BuildingCreaters
{
    public abstract class BuildingRealCreater : IRealCreater
    { 
        public abstract IRealProduct FactoryCreate(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
    }
}
