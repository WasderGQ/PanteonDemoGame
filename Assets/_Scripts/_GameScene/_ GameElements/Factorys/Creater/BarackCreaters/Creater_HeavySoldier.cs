using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;

namespace _Scripts._GameScene.__GameElements.Factorys.Creater.BarackCreaters
{
    public class Creater_HeavySoldier : ICreater
    {
        public IProduct FactoryMethod()
        {
            return new HeavySoldier();




        }
    }
}
