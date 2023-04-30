using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public class HeavySoldier : Soldier
    {
        [SerializeField]private SoldierTypeData _soldierTypes;
        protected Vector2Int _startPositionByCell;
        protected Vector2Int _endPositionByCell;
        
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
            
        }

        private void OnStartSetPositions(Vector2Int startPositionByCell,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }
        protected override void SetMaxHealthOnStart()
        {
            _maxHealth = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.HeavySoldier].Health;
        }

        protected override void SetDamageOnStart()
        {
            _damage = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.HeavySoldier].Damage;
        }
    }
}

