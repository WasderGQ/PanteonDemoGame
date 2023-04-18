using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.PanteonDemo.GameElements.Products;
using WasderGQ.PanteonDemo.GameElements.Products.Soldiers;

namespace WasderGQ.PanteonDemo.GameElements.Factory
{
    public class PowerPlant : Factory
    {
        Electric
        
        public override IProduct MakeProduct(int amount)
        {
            return new Electric();

        }
    }
}