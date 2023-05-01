using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public interface IRealProduct : IProduct 
    {
        public Transform MyTransform { get; }
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
        public static Vector3Int GameObjectSizeByCell { get;}
    }
}

