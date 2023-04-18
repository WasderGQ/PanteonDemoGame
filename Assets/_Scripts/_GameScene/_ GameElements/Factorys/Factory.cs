using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.PanteonDemo.GameElements.Products;

namespace WasderGQ.PanteonDemo.GameElements.Factory
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct MakeProduct();
        
    }

    
    

}
