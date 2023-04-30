using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products
{
    public interface IRealProduct : IGameSpaceOccupanter
    {
        public Transform MyTransform { get; }
        public List<IRealProduct> ProductList { get; }

       
    }
}

