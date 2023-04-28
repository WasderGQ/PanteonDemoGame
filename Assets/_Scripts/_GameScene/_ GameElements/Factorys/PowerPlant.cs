using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._GameArea;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant> , IPortable ,IProduct
    {
        private readonly static  Vector2Int _staticGameObjectSizeByCell = new Vector2Int(2, 3);
        
        [SerializeField] private GameSpace _gameSpace;
        
        public static Vector2Int GameObjectSizeByCell
        {
            get { return _staticGameObjectSizeByCell; }
        }

        private List<IProduct> _product;

        private ICreater _creater;

        public void InIt()
        {
            
            
            
        }
        
        
        
        
        
        
        
        
    }
}