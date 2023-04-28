using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public abstract class SoldierCreater : MonoBehaviour,ICreater
    {
        public abstract IProduct FactoryMethod();
    }
}

