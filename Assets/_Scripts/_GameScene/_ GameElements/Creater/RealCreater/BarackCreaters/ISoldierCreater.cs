using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public abstract class SoldierCreater : MonoBehaviour,IRealCreater
    {
        public abstract IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
        
    }
}

