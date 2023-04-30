using _Scripts._GameScene.__GameElements.Creater.RealCreater;
using _Scripts._GameScene.__GameElements.Products.VirtualProduct;

namespace _Scripts._GameScene.__GameElements.Creater.VirtualCreater.PowerPlantCreaters
{
    public class ElectricRealCreater : IVirtualCreater
    {
        public IVirtualProduct FactoryMethod()
        {
            return new Electric();

        }
    
    }
}
