using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IVulnerable 
    {
        public int CurrentHealth { get; }
        public void TakeDamage(IAttacker attacker);
       
        
    } 
}

