using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IVulnerable 
    {
        public int CurrentHealth { get; }
        public UnityEvent<IAttacker> EventTakeDamage { get; }
        public void TakeDamage(IAttacker attacker);
       
        
    } 
}

