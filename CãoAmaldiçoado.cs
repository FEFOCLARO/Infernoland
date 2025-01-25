namespace InfernoGame
{
    public class CãoAmaldiçoado : Enemy
    {
        public CãoAmaldiçoado() : base("Cão Amaldiçoado", 60, 8, "Sombrio") { }

        public override void Attack(PlayerStats player)
        {
            Console.WriteLine($"{Name} avança com uma mordida sombria!");
            player.ChangeHP(-Damage);
        }
    }
}
