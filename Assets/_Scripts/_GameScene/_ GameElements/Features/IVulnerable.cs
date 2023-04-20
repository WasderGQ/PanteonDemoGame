namespace _Scripts._GameScene.__GameElements.Features
{
    public abstract class IVulnerable
    {
        private int _health;
        
        public int health
        {
            get => _health;
        }

        public void TakeDamage(int damage)
        {

            _health -= damage;

        }
    } 
}

