using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.PanteonDemo.GameElements;

namespace WasderGQ.PanteonDemo.GameElements
{
    public abstract class Soldier : IProduct , IAttacker  
    {
        public int Damage { get; }
        
        
        public void GiveDamage(IVulnerable vulnerable)
        {
            vulnerable.TakeDamage(Damage);

        }
        
    }
}

