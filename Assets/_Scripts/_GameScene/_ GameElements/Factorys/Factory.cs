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
        
        private List<IRealProduct> _product;

        private IRealCreater realCreater;

    }
    
    public abstract class FactoryHave2Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IRealProduct> _product;

        private IRealCreater realCreater;
        
        private List<IRealProduct> _product1;

        private IRealCreater _creater1;
    }
    public abstract class FactoryHave3Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IRealProduct> _product;

        private IRealCreater realCreater;
        
        private List<IRealProduct> _product1;

        private IRealCreater _creater1;
        
        private List<IRealProduct> _product2;

        private IRealCreater _creater2;
        
        
        
        
    }
    public abstract class FactoryHave4Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IRealProduct> _product;

        private IRealCreater realCreater;
        
        private List<IRealProduct> _product1;

        private IRealCreater _creater1;
        
        private List<IRealProduct> _product2;

        private IRealCreater _creater2;
        
        private List<IRealProduct> _product3;

        private IRealCreater _creater3;
        
        
    }
    public abstract class FactoryHave5Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IRealProduct> _product;

        private IRealCreater realCreater;
        
        private List<IRealProduct> _product1;

        private IRealCreater _creater1;
        
        private List<IRealProduct> _product2;

        private IRealCreater _creater2;
        
        private List<IRealProduct> _product3;

        private IRealCreater _creater3;
        
        private List<IRealProduct> _product4;

        private IRealCreater _creater4;
        
    }
    public abstract class FactoryHave6Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IRealProduct> _product;

        private IRealCreater realCreater;
        
        private List<IRealProduct> _product1;

        private IRealCreater _creater1;
        
        private List<IRealProduct> _product2;

        private IRealCreater _creater2;
        
        private List<IRealProduct> _product3;

        private IRealCreater _creater3;
        
        private List<IRealProduct> _product4;

        private IRealCreater _creater4;
        
        private List<IRealProduct> _product5;

        private IRealCreater _creater5;
        
        
    }
   
    
    
    public abstract class Factory : MonoBehaviour
    {
        

        //There must be at least one Mold and one Product, if desired, more than one. Because of that you mustn use generic class

    }

    
    

}
