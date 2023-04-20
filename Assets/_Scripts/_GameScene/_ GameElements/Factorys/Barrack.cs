using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._Logic;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    
  
    
    
    public class Barrack : FactoryHave3Creater<Barrack>
    {
        private Barrack _prefabBarrack;
            
        private List<IProduct> _lightSoldier;
        
        private List<IProduct> _mediumSoldier;
        
        private List<IProduct> _heavySoldier;

        private ICreater _lightSoldierCreater;

        private ICreater _mediumSoldierCreater;

        private ICreater _heavySoldierCreater;




       
            
        
        
        
        
        
        
        
    }
}
