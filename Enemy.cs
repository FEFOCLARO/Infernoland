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

        // Método para receber dano
        public virtual void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
        }

        // Método abstrato para atacar
        public abstract void Attack(PlayerStats player);

        // Método virtual para obter drops
        public virtual string GetDrop()
        {
            return "Nada";  // Substituído nas subclasses
        }
    }
}
