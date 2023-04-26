using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant>
    {
        public static readonly Vector3Int CellSize = new Vector3Int(2, 3);
        private List<IProduct> _product;

        private ICreater _creater;
        
        
        
    }
}