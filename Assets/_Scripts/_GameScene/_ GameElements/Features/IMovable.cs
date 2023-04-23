using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IMovable
    {
       public Vector3 MyStartWorldPosition { get; }
       public Vector3 MyEndWorldPosition { get; }
       public Vector3 MySizeOccupiedInSpace { get; }
    }
}