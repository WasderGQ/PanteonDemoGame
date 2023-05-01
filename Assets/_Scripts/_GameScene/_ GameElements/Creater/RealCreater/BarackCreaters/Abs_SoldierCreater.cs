using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters
{
    public abstract class Abs_SoldierCreater :IRealCreater
    {
        public abstract  IRealProduct FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell);
    }
}

