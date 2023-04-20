using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class PowerPlant : FactoryHave1Creater<PowerPlant>
    {
        private List<IProduct> _product;

        private ICreater _creater;
        
        
        
    }
}