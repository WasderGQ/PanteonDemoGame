using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using Codice.Client.BaseCommands.BranchExplorer.ExplorerData;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products
{
    public interface IProduct : IGameSpaceOccupanter
    {
        public Transform MyTransform { get; }
        public List<IProduct> BuildingProductList { get; }

       
    }
}

