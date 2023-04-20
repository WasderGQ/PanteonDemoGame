using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
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

        private ICreater _creater;
        
        private List<IProduct> _product1;

        private ICreater _creater1;
        
        private List<IProduct> _product2;

        private ICreater _creater2;
        
        
        
        
    }
    public abstract class FactoryHave4Creater<T> : Factory where T : MonoBehaviour
    {
        private List<IProduct> _product;

        private ICreater _creater;
        
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

        private ICreater _creater;
        
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

        private ICreater _creater;
        
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
   
    
    
    public abstract class Factory : MonoBehaviour
    {
        private ICreater[] _ıCreater;

        //There must be at least one Mold and one Product, if desired, more than one. Because of that you mustn use generic class

    }

    
    

}
