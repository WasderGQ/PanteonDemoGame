using System;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public class HeavySoldier : Soldier
    {
        [SerializeField]private SoldierTypeData _soldierTypes;
        
        
        public void InIt()
        {
            AbstractInIt();
            SetMaxHealthOnStart();


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

