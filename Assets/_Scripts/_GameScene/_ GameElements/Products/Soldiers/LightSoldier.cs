using System.Collections.Generic;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    
    public  class LightSoldier : Soldier 
    {
        [SerializeField]private SoldierTypeData _soldierTypes;
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;

        private List<IRealProduct> _products;
        public override Transform MyTransform { get => this.transform; }
        public override List<IRealProduct> ProductList
        {
            get => _products;
        }
        public override Vector2Int StartPositionByCell
        {
            get => _startPositionByCell;
        }
        public override Vector2Int EndPositionByCell
        {
            get => _endPositionByCell;
        }


        public void InIt(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            OnStartSetPositions(startPositionByCell, endPositionByCell);
            SetMaxHealthOnStart();


        }

        private void SetVariable()
        {
            _products = new List<IRealProduct>();
        }
        private void OnStartSetPositions(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }
        protected override void SetMaxHealthOnStart()
        {
            base._maxHealth = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.HeavySoldier].Health;
        }

        protected override void SetDamageOnStart()
        {
            base._damage = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.HeavySoldier].Damage;
        }
    }
    
}

