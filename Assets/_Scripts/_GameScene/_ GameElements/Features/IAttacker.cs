
namespace _Scripts._GameScene.__GameElements.Features
{
    public interface IAttacker
    {
        public int Damage { get; }
        
        
        public void GiveDamage(IVulnerable vulnerable)
        {
            vulnerable.TakeDamage(Damage);

        }
    }
}



