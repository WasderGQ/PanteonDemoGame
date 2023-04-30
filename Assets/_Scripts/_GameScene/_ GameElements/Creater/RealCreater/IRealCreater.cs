using System;
using System.Numerics;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts._GameScene.__GameElements.Creater
{
    public interface IRealCreater
    {
        public IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
    }
}
