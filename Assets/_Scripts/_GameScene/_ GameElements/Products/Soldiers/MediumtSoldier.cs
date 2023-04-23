using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    
    public  class MediumSoldier : Soldier 
    {
        [SerializeField]private SoldierTypeData _soldierTypes;

        protected override void SetMaxHealthOnStart()
        {
            base._maxHealth = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.MediumSoldier].Health;
        }

        protected override void SetDamageOnStart()
        {
            base._damage = _soldierTypes._soldierTypeList[(int)EnumSoldierTyper.MediumSoldier].Damage;
        }
    }
    
}

