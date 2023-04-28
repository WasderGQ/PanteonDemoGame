using _Scripts._GameScene.__GameElements.Products;

namespace _Scripts._GameScene.__GameElements.Creater.BuildCreaters
{
    public abstract class BuildCreater : ICreater
    {
        public abstract IProduct FactoryCreate();
    }
}
