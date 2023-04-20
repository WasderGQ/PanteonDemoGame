using _Scripts._GameScene.__GameElements.Features;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    
    public abstract class Soldier : IProduct , IAttacker
    {
        private int _damage;
        public int Damage
        {
            get => _damage;
        }


    }
}

