using System.Collections.Generic;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IGameSpaceOccupanter
    {
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
        public List<IGameSpaceOccupanter> Occupanters { get; }
    }
}