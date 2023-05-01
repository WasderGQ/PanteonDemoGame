using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.FactoryBuilder.FactoryCreater
{
    public abstract class FactoryCreater1<T1>: FactoryCreater ,ICreater where T1 : IRealProduct
     { 
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T1> CreatedFactoryList1 { get;  }
    }
    public abstract class FactoryCreater2<T1,T2> :  FactoryCreater ,ICreater where T1 : IRealProduct where T2 : IRealProduct
    {
        
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T1> CreatedFactoryList1 { get;  }
        
        public abstract T2 FactoryMethod2(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
       
        public abstract List<T2> CreatedFactoryList2 { get;  }

    }
    
    public abstract class FactoryCreater3<T1,T2,T3> :  FactoryCreater ,ICreater where T1 : IRealProduct where T2 : IRealProduct where T3: IRealProduct
    {
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
       
        public abstract List<T1> CreatedFactoryList1 { get;  }
        
        public abstract T2 FactoryMethod2(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T2> CreatedFactoryList2 { get;  }
    
        public abstract T3 FactoryMethod3(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T3> CreatedFactoryList3 { get;  }
    }
    public abstract class FactoryCreater4<T1,T2,T3,T4> :  FactoryCreater ,ICreater where T1 : IRealProduct where T2 : IRealProduct where T3: IRealProduct  where T4: IRealProduct
    {
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
       
        public abstract List<T1> CreatedFactoryList1 { get;  }
        
        public abstract T2 FactoryMethod2(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T2> CreatedFactoryList2 { get;  }
    
        public abstract T3 FactoryMethod3(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T3> CreatedFactoryList3 { get;  }
       
        public abstract T4 FactoryMethod4(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T4> CreatedFactoryList4 { get;  }
    }
    public abstract class FactoryCreater5<T1,T2,T3,T4,T5> :  FactoryCreater ,ICreater where T1 : IRealProduct where T2 : IRealProduct where T3: IRealProduct  where T4: IRealProduct where T5: IRealProduct
    {
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
       
        public abstract List<T1> CreatedFactoryList1 { get;  }
        
        public abstract T2 FactoryMethod2(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T2> CreatedFactoryList2 { get;  }
    
        public abstract T3 FactoryMethod3(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T3> CreatedFactoryList3 { get;  }
       
        public abstract T4 FactoryMethod4(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T4> CreatedFactoryList4 { get;  }
        
        public abstract T5 FactoryMethod5(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T5> CreatedFactoryList5 { get;  }
    }
    public abstract class FactoryCreater6<T1,T2,T3,T4,T5,T6>:  FactoryCreater ,ICreater where T1 : IRealProduct where T2 : IRealProduct where T3: IRealProduct  where T4: IRealProduct where T5: IRealProduct where T6: IRealProduct
    {
        public abstract T1 FactoryMethod1(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
       
        public abstract List<T1> CreatedFactoryList1 { get;  }
        
        public abstract T2 FactoryMethod2(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T2> CreatedFactoryList2 { get;  }
    
        public abstract T3 FactoryMethod3(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T3> CreatedFactoryList3 { get;  }
       
        public abstract T4 FactoryMethod4(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T4> CreatedFactoryList4 { get;  }
        
        public abstract T5 FactoryMethod5(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T5> CreatedFactoryList5 { get;  }
        
        public abstract T6 FactoryMethod6(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell,Vector2Int productSizeByCell);
        
        public abstract List<T5> CreatedFactoryList6 { get;  }
        
    }
   
   
    
    
    public abstract class FactoryCreater 
    {
       
    }
}
