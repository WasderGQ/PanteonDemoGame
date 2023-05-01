using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.RealCreater.UtilityCreaters
{
    public interface IAttachmentCreater<T> : ICreater
    {
    
        public T FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int prodcutSizeByCell);
    }
}
