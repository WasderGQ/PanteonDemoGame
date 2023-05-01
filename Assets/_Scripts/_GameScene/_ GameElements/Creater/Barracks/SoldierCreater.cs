using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.Barracks
{
    public abstract class SoldierCreater<T> :ICreater where T : Soldier
    {
        public abstract List<T> CreatedSoldierList { get;}
        public abstract T FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell);
    }
}
