namespace WasderGQ.PanteonDemo.GameElements.Products.Soldiers
{
    
    public class MediumSoldier : Soldier 
    {
        private int _damage = 5;

        public new int Damage
        {
            get => _damage;
        }
        public void GiveDamage(IVulnerable vulnerable)
        {
            vulnerable.TakeDamage(Damage);

        }
    }

    
}
