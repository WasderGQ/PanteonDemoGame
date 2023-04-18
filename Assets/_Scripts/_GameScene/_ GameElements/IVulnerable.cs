using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasderGQ.PanteonDemo.GameElements
{
    public abstract class IVulnerable
    {
        private int _health;
        
        public int health
        {
            get => _health;
        }

        public void TakeDamage(int damage)
        {

            _health -= damage;

        }
    } 
}

