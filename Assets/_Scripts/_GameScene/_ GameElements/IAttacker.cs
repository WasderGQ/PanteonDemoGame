using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.PanteonDemo.GameElements;
public interface IAttacker 
{
    public int Damage { get; }
        
        
    public void GiveDamage(IVulnerable vulnerable)
    {
        vulnerable.TakeDamage(Damage);

    }
}
