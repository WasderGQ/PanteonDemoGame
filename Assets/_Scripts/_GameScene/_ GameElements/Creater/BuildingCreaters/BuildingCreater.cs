using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BuildingCreaters
{
    public abstract class BuildingCreater : ICreater
    { 
        public abstract IProduct FactoryCreate(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
    }
}
