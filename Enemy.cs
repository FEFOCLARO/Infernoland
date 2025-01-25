using System;

namespace InfernoGame
{
    public abstract class Enemy
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int Damage { get; protected set; }
        public string Element { get; protected set; }
        protected Random random = new Random();

        public Enemy(string name, int hp, int damage, string element)
        {
            Name = name;
            HP = hp;
            Damage = damage;
            Element = element;
        }

        public virtual void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
        }

        public abstract void Attack(PlayerStats player);

        public virtual string GetDrop()
        {
            int dropChance = random.Next(1, 101);
            return dropChance switch
            {
                <= 10 => "Poção de SP",
                <= 20 => "Espada Sagrada",
                <= 30 => "Arco Abençoado",
                <= 40 => "Cajado de Fogo",
                <= 50 => "Escudo de Madeira",
                _ => "Nada",
            };
        }
    }
}
