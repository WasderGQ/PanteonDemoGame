using _Scripts._GameScene.__GameElements.Features;

namespace _Scripts._GameScene.__GameElements.Products.Soldiers
{
    public class HeavySoldier : Soldier
    {
        private int _damage = 10;

        public int Damage
        {
            get => _damage;
        }
        public void GiveDamage(IVulnerable vulnerable)
        {
            vulnerable.TakeDamage(Damage);

        }

        
    }
}

