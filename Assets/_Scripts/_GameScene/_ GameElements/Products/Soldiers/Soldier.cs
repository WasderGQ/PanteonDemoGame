using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public abstract class Soldier : MonoBehaviour ,IAttacker,IVulnerable,IRealProduct
    {

        #region Abstract Property
        public abstract Transform MyTransform { get; }
        public abstract List<IRealProduct> ProductList { get; }
        public abstract Vector2Int StartPositionByCell { get; }
        public abstract Vector2Int EndPositionByCell { get; }
        public abstract int Damage { get; }
        public abstract int CurrentHealth { get; }
        #endregion
        
        #region Same Property and Variable
        public static Vector2Int GameSpaceSizeByCell { get => _gameSpaceSizeByCell; }
        private static Vector2Int _gameSpaceSizeByCell = new Vector2Int(1, 1);
        
        #endregion
        
        #region Abstract Func.

        protected abstract void OnStartSetPositions(Vector2Int startPositionByCell, Vector2Int endPositionByCell);
        protected abstract void OnStartSetMaxHealth();
        protected abstract void OnStartSetDamageOnStart();
        
        #region Vulnerable Func.
        public abstract void TakeDamage(IAttacker attacker);
        
        #endregion
        
        
        #endregion
        
    }
}

