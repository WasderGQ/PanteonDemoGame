using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IPathFinderMove
    {
        public void Move(Vector2Int movingCell);
    }
}