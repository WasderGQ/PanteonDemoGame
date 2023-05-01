using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers
{
    
    public class MediumSoldier : Soldier ,IAttacker,IVulnerable,IRealProduct
    {
        [SerializeField]private SoldierTypeData _soldierTypes;
        [SerializeField] private int _maxHealth;
        [SerializeField]private int _damage;
        [SerializeField]private int _currentHealth;
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private List<IRealProduct> _products;
        private Vector2Int _mementoBornPlace;
        public override List<IRealProduct> ProductList { get => _products; }
        public override Vector2Int StartPositionByCell { get => _startPositionByCell; }
        public override Vector2Int EndPositionByCell { get => _endPositionByCell; }
        public override int Damage { get => _damage; }
        public override int CurrentHealth { get => _currentHealth; }

        
        public void InIt(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            OnStartSetVariable(startPositionByCell, endPositionByCell);
        }

        
        
        private void OnStartSetVariable(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            OnStartSetPositions(startPositionByCell, endPositionByCell);
            OnStartSetBornedPlace(startPositionByCell);
            OnStartSetDamageOnStart();
            OnStartSetMaxHealth();
            
        }

        private void OnStartSetBornedPlace(Vector2Int startPositionByCell)
        {
            _mementoBornPlace = startPositionByCell;
        }
        
        protected override void OnStartSetPositions(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }
        protected override void OnStartSetMaxHealth()
        {
            _maxHealth = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.MediumSoldier].Health;
            _currentHealth = _maxHealth;
        }

        protected override void OnStartSetDamageOnStart()
        {
            _damage = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.MediumSoldier].Damage;
        }

        public override void TakeDamage(IAttacker attacker)
        {
            _currentHealth = _currentHealth - attacker.Damage;
            CheckAmIDead();
        }

        private void CheckAmIDead()
        {
            if(_currentHealth <= 0)
            {
                Destroy(this);
            }
            
            
            


        }
    }
    
}
