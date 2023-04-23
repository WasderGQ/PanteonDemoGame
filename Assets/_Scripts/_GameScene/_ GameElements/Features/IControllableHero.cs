using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IControllableHero
    {
        Vector3 GameSpacePosition { get; }

        public void Move();

    }
}