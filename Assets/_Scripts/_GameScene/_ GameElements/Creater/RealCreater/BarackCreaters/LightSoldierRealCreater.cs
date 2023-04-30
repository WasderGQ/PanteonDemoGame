using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public class LightSoldierRealCreater : SoldierRealCreater
    {
        public override IRealProduct FactoryMethod()
        {
            return new LightSoldier();
        }
    }
}
