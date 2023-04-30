using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public interface IRealProduct : IProduct 
    {
        public Transform MyTransform { get; }
        public List<IRealProduct> ProductList { get; }
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }

    }
}

