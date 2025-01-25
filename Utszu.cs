namespace InfernoGame
{
    public class Utszú : Enemy
    {
        public Utszú() : base("Utszú", 70, 12, "Sombrio") { }

        public override void Attack(PlayerStats player)
        {
            Console.WriteLine($"{Name} usa Grito Sombrio!");
            player.ChangeHP(-Damage);
        }
    }
}
