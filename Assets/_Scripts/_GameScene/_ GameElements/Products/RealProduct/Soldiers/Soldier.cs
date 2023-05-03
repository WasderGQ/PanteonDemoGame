using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene._PlayerControl;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers
{
    public abstract class Soldier : MonoBehaviour ,IAttacker,IVulnerable,IRealProduct,IMoveByPathFinder
    {
        
        [SerializeField] private SoldierController _soldierController;
        #region Abstract Property
        public abstract List<IRealProduct> ProductList { get; } //Soldier don't have product
       
        public abstract Vector2Int StartPositionByCell { get; }
        public abstract Vector2Int EndPositionByCell { get; }
        
        public abstract int Damage { get; }
        public abstract int CurrentHealth { get; }
        #endregion
        
        #region Same Property and Variable
        public static Vector2Int GameSpaceSizeByCell { get => _gameSpaceSizeByCell; }
        private static Vector2Int _gameSpaceSizeByCell = new Vector2Int(1, 1);
        
        #endregion

        #region Common Func.

        protected void SoldierInIt()
        {
            _soldierController = new SoldierController();

        }
        
        

        #endregion
        
        
        
        
        
        #region Abstract Func.

        public abstract void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell);
        protected abstract void OnStartSetPositions(Vector2Int startPositionByCell, Vector2Int endPositionByCell);
        protected abstract void OnStartSetMaxHealth();
        protected abstract void OnStartSetDamageOnStart();
        
       
        #region Vulnerable Func.
        public abstract void TakeDamage(IAttacker attacker);
        
        #endregion
        
        
        #endregion
        
        
        public void Move(Vector2Int movingCell)
        {
            _soldierController.StartMovement(movingCell);
        }

        protected abstract void CheckAmIDead();

    }
}

