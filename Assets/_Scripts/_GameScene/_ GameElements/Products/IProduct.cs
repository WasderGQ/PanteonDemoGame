using System.Collections.Generic;
using Codice.Client.BaseCommands.BranchExplorer.ExplorerData;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products
{
    public interface IProduct
    {
        public Transform Transform { get; }
        public List<IProduct> BuildingsProductList { get; }
    }
}

