using System.Collections.Generic;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct
{
    public interface IRealProduct : IProduct
    {
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
        public List<IRealProduct> ProductList { get; }
    }
    



}

