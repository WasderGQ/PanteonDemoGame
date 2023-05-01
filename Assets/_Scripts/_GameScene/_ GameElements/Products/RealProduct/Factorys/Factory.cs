using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct.Factorys
{
    public abstract class FactoryHave1Creater<T> : Factory where T : MonoBehaviour 
    {
        
        private List<IProduct> _product;

        private ICreater _creater;

    }
    
    public abstract class FactoryHave2Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater _creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
    }
    public abstract class FactoryHave3Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater  _creater;
        
        private List<IProduct> _product1;

        private ICreater  _creater1;
        
        private List<IProduct> _product2;

        private ICreater  _creater2;
        
    }



    public abstract class Factory : MonoBehaviour 
    {
       
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
    }

    
    

}
