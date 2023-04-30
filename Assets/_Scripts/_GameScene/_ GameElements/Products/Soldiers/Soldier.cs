using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    
    
    public abstract class Soldier : MonoBehaviour ,IAttacker,IVulnerable,IControllableHero,IRealProduct
    {

        public static Vector2Int GameSpaceSizeByCell { get => _gameSpaceSizeByCell; }
        protected int _maxHealth;
        protected Vector3 _gameSpacePosition;
        protected int _damage;
        protected int _currentHealth;

        
        
        private static Vector2Int _gameSpaceSizeByCell = new Vector2Int(1, 1);
        
        public int MaxHealth { get => _maxHealth; }
        public  Vector3 GameSpacePosition { get => _gameSpacePosition; }
        
        public int Damage { get => _damage; }
        public  int CurrentHealth { get => _currentHealth; }
        
        public UnityEvent<IAttacker> EventTakeDamage => new UnityEvent<IAttacker>();

        public UnityEvent MoveEvent;
        public void Move()
        {
            throw new System.NotImplementedException();
        }
        
        #region Start Func.

        public void AbstractInIt(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            SetEvents();
            SetStartHealth();
            AddListener();
        }
        
        
        private void AddListener()
        {
            EventTakeDamage.AddListener(TakeDamage);
            MoveEvent.AddListener(Move);
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

        public abstract Transform MyTransform { get; }
        public abstract List<IRealProduct> ProductList { get; }
        public abstract Vector2Int StartPositionByCell { get; }
        public abstract Vector2Int EndPositionByCell { get; }
        
    }
}

