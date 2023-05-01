using _Scripts._GameScene.__GameElements.Products.VirtualProduct;

namespace _Scripts._GameScene.__GameElements.Creater.VirtualCreater
{
    public class ElectricProductCreater : IVirtualProductCreater<Electric>
    {
        public Electric FactoryMethod()
        {
            return new Electric();

        }
    
    }
}
