using _Scripts._GameScene.__GameElements.Products.VirtualProduct;

namespace _Scripts._GameScene.__GameElements.Creater.VirtualCreater
{
    public interface IVirtualProductCreater<T> : ICreater
    {
        public T FactoryMethod();
        
    }
}