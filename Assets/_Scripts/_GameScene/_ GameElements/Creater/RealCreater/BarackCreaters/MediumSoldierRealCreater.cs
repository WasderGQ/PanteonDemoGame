using System;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public class MediumSoldierRealCreater : SoldierRealCreater
    {
        public override IRealProduct FactoryMethod()
        {
            return new MediumSoldier();
        }
    }
}
