using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IMovable
    {
        public void Move(Vector3 mousePosition);
        public void MoveDefault();
        public void TrueMove(Vector3 mousePosition);
    }
}