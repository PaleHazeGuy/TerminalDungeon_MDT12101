using System;

namespace TerminalDungeon
{
  class Program
  {
    static void RenderUI(Hero player, Enemy enemy, string combatLog)
    {
      Console.Clear();
      Console.WriteLine("========================================");
      Console.WriteLine("            TERMINAL DUNGEON            ");
      Console.WriteLine("========================================");
      Console.WriteLine($"[PLAYER] {player.Name} ({player.GetType().Name} Lv.{player.Level})");
      Console.WriteLine($"         HP: {player.HP}/{player.MaxHP} | MP: {player.MP}/{player.MaxMP}");
      Console.WriteLine($"[ENEMY]  {enemy.Name}");
      Console.WriteLine($"         HP: {enemy.HP}/{enemy.MaxHP} | MP: {enemy.MP}/{enemy.MaxMP}");
      Console.WriteLine("----------------------------------------");
      Console.WriteLine($"[LAST TURN]: {combatLog}");
      Console.WriteLine("----------------------------------------");
    }

    static void Main(string[] args)
    {
      Console.Clear();
      Console.WriteLine("========================================");
      Console.WriteLine("      WELCOME TO TERMINAL DUNGEON       ");
      Console.WriteLine("========================================");

      string name = "";
      while (string.IsNullOrWhiteSpace(name))
      {
        Console.Write("Enter your Hero's name: ");
        name = Console.ReadLine();
      }

      Hero player = null;
      while (player == null)
      {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine($"  Choose a Character Class for {name}   ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Warrior - High HP, Solid physical skills.");
        Console.WriteLine("2. Mage    - Low HP, Massive mana pools and spells.");
        Console.WriteLine("3. Rogue   - Balanced stats, high crit potential.");
        Console.WriteLine("----------------------------------------");
        Console.Write("Select Class (1-3): ");

        string classChoice = Console.ReadLine();
        if (classChoice == "1") player = new Warrior(name);
        else if (classChoice == "2") player = new Mage(name);
        else if (classChoice == "3") player = new Rogue(name);
        else
        {
          Console.WriteLine("\nInvalid selection! Press any key to try again...");
          Console.ReadKey(true);
        }
      }

      // Instantiating our newly extracted subclass enemy cleanly!
      Enemy enemy = new DungeonGuardian();

      player.Inventory.Add(new Potion("Health Potion", "Restores 50 HP.", 50, 0));
      player.Inventory.Add(new Potion("Mana Potion", "Restores 25 MP.", 0, 25));
      player.Inventory.Add(new Potion("Rejuvenation Elixir", "Restores 30 HP and 15 MP.", 30, 15));

      string combatLog = $"Battle Started! A hostile {enemy.Name} blocks your path.";

      while (player.IsAlive && enemy.IsAlive)
      {
        RenderUI(player, enemy, combatLog);

        bool validAction = false;
        bool isPlayerBlocking = false;

        while (!validAction)
        {
          Console.WriteLine("\nChoose your action:");
          Console.WriteLine("1. Attack");
          Console.WriteLine("2. Use Skill");
          Console.WriteLine("3. Block (Reduce damage & Regen 5 MP)");
          Console.WriteLine("4. Open Inventory");
          Console.Write("> ");

          string input = Console.ReadLine();

          if (input == "1")
          {
            combatLog = player.Attack(enemy);
            validAction = true;
          }
          else if (input == "2")
          {
            int skillCost = player is Mage ? 20 : 15;
            if (player.MP >= skillCost)
            {
              combatLog = player.UseSkill(enemy);
              validAction = true;
            }
            else
            {
              Console.WriteLine("Not enough MP! Press any key to refresh...");
              Console.ReadKey(true);
              RenderUI(player, enemy, combatLog);
            }
          }
          else if (input == "3")
          {
            isPlayerBlocking = true;
            player.MP += 5;
            combatLog = $"{player.Name} raised their guard and gathered energy (+5 MP).";
            validAction = true;
          }
          else if (input == "4")
          {
            if (player.Inventory.Count == 0)
            {
              Console.WriteLine("Your inventory is empty! Press any key to continue... ");
              Console.ReadKey(true);
              RenderUI(player, enemy, combatLog);
            }
            else
            {
              Console.Clear();
              Console.WriteLine("========================================");
              Console.WriteLine("               INVENTORY                ");
              Console.WriteLine("========================================");
              for (int i = 0; i < player.Inventory.Count; i++)
              {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} - {player.Inventory[i].Description}");
              }
              Console.WriteLine("----------------------------------------");
              Console.Write("Choose an item number to use (or 0 to go back): ");

              if (int.TryParse(Console.ReadLine(), out int inventoryChoice))
              {
                if (inventoryChoice == 0)
                {
                  RenderUI(player, enemy, combatLog);
                }
                else if (inventoryChoice > 0 && inventoryChoice <= player.Inventory.Count)
                {
                  combatLog = player.UseItem(inventoryChoice - 1);
                  validAction = true;
                }
                else
                {
                  Console.WriteLine("Invalid item slot selected. Press any key to return...");
                  Console.ReadKey(true);
                  RenderUI(player, enemy, combatLog);
                }
              }
              else
              {
                Console.WriteLine("Invalid input. Press any key to return...");
                Console.ReadKey(true);
                RenderUI(player, enemy, combatLog);
              }
            }
          }
          else
          {
            Console.WriteLine("Invalid choice! Press any key to refresh...");
            Console.ReadKey(true);
            RenderUI(player, enemy, combatLog);
          }
        }

        if (!enemy.IsAlive)
        {
          RenderUI(player, enemy, combatLog);
          Console.WriteLine($"\nVictory! {enemy.Name} has been defeated!");
          Console.WriteLine(player.GainEXP(enemy.RewardEXP));
          break;
        }

        bool isEnemyBlocking;
        string enemyResponse = enemy.TakeTurn(player, isPlayerBlocking, out isEnemyBlocking);
        combatLog += $" | {enemyResponse}";

        if (!player.IsAlive)
        {
          RenderUI(player, enemy, combatLog);
          Console.WriteLine($"\nGame Over... {player.Name} was defeated.");
          break;
        }

        RenderUI(player, enemy, combatLog);
        Console.WriteLine("\nPress any key to proceed to the next turn...");
        Console.ReadKey(true);
      }

      Console.WriteLine("\n========================================");
      Console.WriteLine("             SESSION ENDED              ");
      Console.WriteLine("========================================");
      Console.ReadLine();
    }
  }
}
