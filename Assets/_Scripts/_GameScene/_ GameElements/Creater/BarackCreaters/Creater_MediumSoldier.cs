using System;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public class Creater_MediumSoldier : SoldierCreater
    {
        public override IProduct FactoryMethod()
        {
            return new MediumSoldier();
        }
    }
}
