using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products;

namespace _Scripts._GameScene.__GameElements.Creater.BuildCreaters
{
    public class PowerPlantCreater : BuildCreater
    {
        public override IProduct FactoryCreate()
        {
            return new PowerPlant();
        }
    }
}
