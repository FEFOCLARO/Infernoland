namespace InfernoGame
{
    public class ShadowMinion : Enemy
    {
        public ShadowMinion() : base("Sombra Fraca", 50, 5, "Neutro") { }

        public override void Attack(PlayerStats player)
        {
            Console.WriteLine($"{Name} ataca com garras sombrias!");
            player.ChangeHP(-Damage);  // Aplica o dano ao jogador
        }

        public override string GetDrop()
        {
            if (random.Next(1, 101) <= 50)  // 50% de chance de drop
            {
                Console.WriteLine($"{Name} deixou cair uma Poção de Cura!");
                return "Poção de Cura";
            }
            Console.WriteLine($"{Name} não deixou nada para trás.");
            return "Nada";
        }
    }
}
