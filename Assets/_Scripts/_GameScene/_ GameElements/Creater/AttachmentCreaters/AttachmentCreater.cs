using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.AttachmentCreaters
{
    public abstract class AttachmentCreater<T> : ICreater
    {
    
        public abstract T FactoryMethod(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int prodcutSizeByCell);
    }
}
