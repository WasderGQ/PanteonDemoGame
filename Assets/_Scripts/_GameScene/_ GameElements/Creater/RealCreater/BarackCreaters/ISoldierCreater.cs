using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public abstract class SoldierRealCreater : MonoBehaviour,IRealCreater
    {
        public abstract IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
    }
}

