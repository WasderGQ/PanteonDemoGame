using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.ProducibleValuables
{
    public abstract class ProducibleVariable : IProduct
    {
        public Transform MyTransform { get; }
       
        public List<IProduct> BuildingProductList { get; }
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
        public List<IGameSpaceOccupanter> Occupanters { get; }
    }
}
