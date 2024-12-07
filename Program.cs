using System;
using Microsoft.Win32.SafeHandles;

namespace InfernoGame
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            PlayerStats player = CreateCharacter();
            Console.Clear();

            // Diálogo com o boss final
            Console.WriteLine($"\"Aqui quem fala é boss final, estou no último círculo deste inferno te esperando, {player.Name}.\"");
            Console.WriteLine($"\"Para meu divertimento, lhe darei este {player.Weapon} para você tentar a sorte contra meus escravos.\"");
            Console.WriteLine("\"ዲያብሎስ ክበልዓካ እዩ።\"");
            Console.WriteLine($"Você recebeu: {player.Weapon} (Dano: {player.WeaponDamage}).");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            // Iniciar o primeiro nível
            StartFirstLevel(player);
        }

        static PlayerStats CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("       BEM-VINDO À INFERNOLAND!        ");
            Console.WriteLine("=======================================");

            Console.Write("Nos diga como devemos lhe chamar: ");
            string name = Console.ReadLine();

            string playerClass = ChooseClass();
            int hp = 0, sp = 0;

            switch (playerClass)
            {
                case "Arqueiro":
                    hp = 250;
                    sp = 150;
                    break;
                case "Guerreiro":
                    hp = 400;
                    sp = 50;
                    break;
                case "Mago":
                    hp = 150;
                    sp = 300;
                    break;
            }

            PlayerStats player = new PlayerStats(name, playerClass, hp, sp);
            player.GiveInitialWeapon();
            return player;
        }

        static string ChooseClass()
        {
            Console.WriteLine("Escolha sua classe:");
            Console.WriteLine("1. Arqueiro (250 HP / 150 SP)");
            Console.WriteLine("2. Guerreiro (400 HP / 50 SP)");
            Console.WriteLine("3. Mago (150 HP / 300 SP)");
            Console.Write("Digite o número correspondente à sua escolha: ");

            string choice = Console.ReadLine();

            return choice switch
            {
                "1" => "Arqueiro",
                "2" => "Guerreiro",
                "3" => "Mago",
                _ => ChooseClass(),
            };
        }

        static void StartFirstLevel(PlayerStats player)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("             NÍVEL 1: LIMBOLIC         ");
            Console.WriteLine("=======================================");
            Console.WriteLine("Você entrou no primeiro círculo do inferno...");
            Console.WriteLine("Um monstro aparece à sua frente!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            // Criar o primeiro inimigo
            Enemy firstEnemy = new ShadowMinion();  // Instancia uma subclasse de Enemy

            // Iniciar o combate por turnos
            Combat(player, firstEnemy);

        }

        static void Combat(PlayerStats player, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine($"Você está enfrentando {enemy.Name} (HP: {enemy.HP}, Dano: {enemy.Damage})!");

            while (player.HP > 0 && enemy.HP > 0)
            {
              Console.WriteLine("\n=======================================");
              Console.WriteLine($"Seu HP: {player.HP} | HP do {enemy.Name}: {enemy.HP}");
              Console.WriteLine("=======================================");

        // Turno do jogador
        Console.WriteLine("Escolha sua ação:");
        Console.WriteLine("1. Atacar (10 de Dano) (80% de chance de acerto) (15% acerto crítico)");
        Console.WriteLine("2. Defender (Reduz dano sofrido pela metade)");
        Console.WriteLine("3. Tomar Poção (Restaura 50 HP)");
        Console.Write("Digite sua escolha: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
                ExecutePlayerAttack(player, enemy);

    // Verifica se o inimigo foi derrotado após o ataque do jogador
             if (enemy.HP <= 0)
                 {
        Console.WriteLine($"\nVocê derrotou {enemy.Name}!");

        // Verifica se o inimigo deixou um item
        string drop = enemy.GetDrop();  
        if (drop != "Nada")
        {
            player.AddItem(drop);  // Adiciona o item ao inventário
        }
        break;  // Sai do loop de combate
    }
}
        else if (choice == "2")
        {
            Console.WriteLine($"\nVocê assume uma postura defensiva.");
            player.IsDefending = true;
        }
        else if (choice == "3")
        {
            if (player.Items.Contains("Poção de Cura"))
            {
                Console.WriteLine("\nVocê tomou uma Poção de Cura e recuperou 50 HP!");
                player.ChangeHP(50);
                player.Items.Remove("Poção de Cura");
            }
            else
            {
                Console.WriteLine("\nVocê não tem poções no momento!");
            }
        }
        else
        {
            Console.WriteLine("\nOpção inválida. Você perdeu seu turno.");
        }

        // Turno do inimigo (se ainda estiver vivo)
        if (enemy.HP > 0)
        {
            enemy.Attack(player);

            // Verifica se o jogador foi derrotado após o ataque do inimigo
            if (player.HP <= 0)
            {
                Console.WriteLine("\nVocê foi derrotado...");
                Console.WriteLine("Game Over!");
                return;  // Sai do método Combat
            }
        }
    }

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
}

        static void ExecutePlayerAttack(PlayerStats player, Enemy enemy)
        {
            // Chance de acerto
            if (random.Next(1, 101) > 80)
            {
                Console.WriteLine("Você errou o ataque!");
                return;
            }

            // Chance de evasão do inimigo
            if (random.Next(1, 101) <= 20)
            {
                Console.WriteLine($"{enemy.Name} esquivou do seu ataque!");
                return;
            }

            // Chance de dano crítico
            int damage = player.WeaponDamage;
            if (random.Next(1, 101) <= 15)
            {
                damage *= 2;
                Console.WriteLine("GOLPE CRÍTICO!");
            }

            enemy.TakeDamage(damage);
            Console.WriteLine($"Você causou {damage} de dano a {enemy.Name}!");
        }

        static void ExecuteEnemyAttack(PlayerStats player, Enemy enemy)
        {
            int damage = enemy.Damage;

            if (player.IsDefending)
            {
                damage /= 2;
                player.IsDefending = false;
                Console.WriteLine("Você defendeu o ataque!");
            }

            player.ChangeHP(-damage);
            Console.WriteLine($"{enemy.Name} causou {damage} de dano a você!");
        }
    }
}
