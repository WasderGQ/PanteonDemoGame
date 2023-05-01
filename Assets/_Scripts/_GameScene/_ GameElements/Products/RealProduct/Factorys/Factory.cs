using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Creater.RealCreater;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public abstract class FactoryHave1Creater<T> : Factory where T : MonoBehaviour 
    {
        
        private List<IProduct> _product;

        private ICreater creater;

    }
    
    public abstract class FactoryHave2Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
    }
    public abstract class FactoryHave3Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
        
        private List<IProduct> _product2;

        private ICreater _creater2;
        
        
        
        
    }
    public abstract class FactoryHave4Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
        
        private List<IProduct> _product2;

        private ICreater _creater2;
        
        private List<IProduct> _product3;

        private ICreater _creater3;
        
        
    }
    public abstract class FactoryHave5Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
        
        private List<IProduct> _product2;

        private ICreater _creater2;
        
        private List<IProduct> _product3;

        private ICreater _creater3;
        
        private List<IProduct> _product4;

        private ICreater _creater4;
        
    }
    public abstract class FactoryHave6Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
        
        private List<IProduct> _product2;

        private ICreater _creater2;
        
        private List<IProduct> _product3;

        private ICreater _creater3;
        
        private List<IProduct> _product4;

        private ICreater _creater4;
        
        private List<IProduct> _product5;

        private ICreater _creater5;
        
        
    }
   
    
    
    public abstract class Factory : MonoBehaviour ,IRealProduct
    {
        public Transform MyTransform { get; }
        public List<IRealProduct> ProductList { get; }
        public Vector2Int StartPositionByCell { get; }
        public Vector2Int EndPositionByCell { get; }
    }

    
    

}
