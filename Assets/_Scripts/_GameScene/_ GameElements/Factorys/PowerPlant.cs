using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant> , IPortable
    {
        private readonly static  Vector2Int _staticGameObjectSizeByCell = new Vector2Int(2, 3);
        public static Vector2Int GameObjectSizeByCell
        {
            get { return _staticGameObjectSizeByCell; }
        }
        
        
        
        
        
        
        public static readonly Vector3Int CellSize = new Vector3Int(2, 3);
        private List<IProduct> _product;

        private ICreater _creater;
        
        
        
    }
}