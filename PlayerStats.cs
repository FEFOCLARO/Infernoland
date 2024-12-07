using System;
using System.Collections.Generic;

namespace InfernoGame
{
    public class PlayerStats
    {
        public string Name { get; private set; }
        public string Class { get; private set; }
        public int HP { get; private set; }
        public int SP { get; private set; }
        public string Weapon { get; private set; }
        public int WeaponDamage { get; private set; }
        public bool IsDefending { get; set; }
        
        // Adicionado: Inventário de Itens
        public List<string> Items { get; private set; }

        public PlayerStats(string name, string playerClass, int hp, int sp)
        {
            Name = name;
            Class = playerClass;
            HP = hp;
            SP = sp;
            Weapon = "Nenhuma";
            WeaponDamage = 0;
            IsDefending = false;
            
            // Inicializa o inventário vazio
            Items = new List<string>();
        }

        public void GiveInitialWeapon()
        {
            switch (Class)
            {
                case "Arqueiro": Weapon = "Arco"; WeaponDamage = 10; break;
                case "Guerreiro": Weapon = "Espada"; WeaponDamage = 10; break;
                case "Mago": Weapon = "Cajado"; WeaponDamage = 10; break;
            }
        }

        public void ChangeHP(int amount)
        {
            HP += amount;
            if (HP < 0) HP = 0; // Garante que o HP não fique negativo
        }
        
        public void AddItem(string item)
        {
            Items.Add(item);
            Console.WriteLine($"{item} foi adicionado ao seu inventário.");
        }
    }
}
