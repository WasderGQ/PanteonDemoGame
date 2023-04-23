using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    
    
    public abstract class Soldier : MonoBehaviour,IAttacker,IVulnerable,IProduct,IControllableHero
    {
        
        
        protected int _maxHealth;
        protected Vector3 _gameSpacePosition;
        protected int _damage;
        protected int _currentHealth;
        
        public  int MaxHealth
        {
            get => _maxHealth;
        }
        public  Vector3 GameSpacePosition
        {
            get => _gameSpacePosition;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public int Damage
        {
            get => _damage;
        }
        public  int CurrentHealth
        {
            get => _currentHealth;
        }
        
        
        public UnityEvent<IAttacker> EventTakeDamage => new UnityEvent<IAttacker>();

        
        #region Start Func.

        public void AbstractInIt()
        {
            SetEvents();
            SetStartHealth();
        }
        protected void SetEvents()
        {
            EventTakeDamage.AddListener(TakeDamage);
        }
        protected void SetStartHealth()
        {
            _currentHealth = _maxHealth;
        }
        
        #endregion
        
        #region Abstract Func.

        protected abstract void SetMaxHealthOnStart();
        protected abstract void SetDamageOnStart();
        
        #endregion

        #region Attacker Func.

        public void GiveDamage(IVulnerable vulnerable)
        {
            vulnerable.EventTakeDamage.Invoke(this);
        }

        #endregion

        #region Vulnerable Func.

        
        
        
        public void TakeDamage(IAttacker attacker)
        {
            _currentHealth -= attacker.Damage;
        }

        
        

        #endregion
        

        
    }
}

