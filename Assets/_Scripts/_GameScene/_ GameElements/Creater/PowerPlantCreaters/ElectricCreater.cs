using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.ProducibleValuables;

namespace _Scripts._GameScene.__GameElements.Creater.PowerPlantCreaters
{
    public class ElectricCreater : ICreater
    {
        public IProduct FactoryMethod()
        {
            return new Electric();

        }
    
    }
}
