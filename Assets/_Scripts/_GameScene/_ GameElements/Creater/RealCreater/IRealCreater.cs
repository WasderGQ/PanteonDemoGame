using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater
{
    public interface IRealCreater : ICreater
    {
        public IRealProduct FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell);
        
        
    }

    
}
