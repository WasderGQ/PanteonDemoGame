using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters
{
    public abstract class Abs_BuildingCreater :IRealCreater
    { 
        public abstract IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
    }
}
