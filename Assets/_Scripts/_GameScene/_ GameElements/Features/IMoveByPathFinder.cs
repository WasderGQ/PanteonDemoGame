using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IMoveByPathFinder
    {
        public void Move(Vector2Int movingCell);
    }
}