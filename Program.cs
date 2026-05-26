using System;

namespace TerminalDungeon
{
  public class TestHero : Hero
  {
    private int _mp;
    public int MaxMP { get; private set; }

    public int MP
    {
      get => _mp;
      set => _mp = Math.Clamp(value, 0, MaxMP);
    }

    public TestHero(string name, int maxHp, int atk) : base(name, maxHp, atk)
    {
      MaxMP = 40;
      MP = 20;
    }

    public override void UseSkill(Character target)
    {
      int mpCost = 15;
      if (MP >= mpCost)
      {
        MP -= mpCost;
        int skillDamage = ATK * 2;
        Console.WriteLine($"\n[Skill] {Name} casts a heavy strike! (Used {mpCost} MP)");
        target.HP -= skillDamage;
        Console.WriteLine($"{target.Name} took {skillDamage} damage!");
      }
    }
  }

  public class TestEnemy : Character
  {
    private int _mp;
    public int MaxMP { get; private set; }

    public int MP
    {
      get => _mp;
      set => _mp = Math.Clamp(value, 0, MaxMP);
    }

    private Random _rng = new Random();

    public TestEnemy(string name, int hp, int atk) : base(name, hp, atk)
    {
      MaxMP = 30;
      MP = 10;
    }

    public override void Attack(Character target)
    {
    }

    public void TakeTurn(Character target, bool isTargetBlocking, out bool isEnemyBlocking)
    {
      isEnemyBlocking = false;
      int choice = _rng.Next(1, 4);

      if (choice == 2 && MP < 10)
      {
        choice = 1;
      }

      if (choice == 1)
      {
        int damage = this.ATK;
        if (isTargetBlocking)
        {
          damage = (int)(this.ATK * 0.3);
          Console.WriteLine($"\n[Enemy Turn] {Name} lunges forward, but {target.Name} blocks the hit!");
        }
        else
        {
          Console.WriteLine($"\n[Enemy Turn] {Name} lunges forward with a basic attack!");
        }
        target.HP -= damage;
        Console.WriteLine($"{target.Name} took {damage} damage!");
      }
      else if (choice == 2)
      {
        MP -= 10;
        int damage = this.ATK + 12;
        if (isTargetBlocking)
        {
          damage = (int)(damage * 0.3);
          Console.WriteLine($"\n[Enemy Turn] {Name} unleashes a Dark Blast! {target.Name} mitigates the impact!");
        }
        else
        {
          Console.WriteLine($"\n[Enemy Turn] {Name} unleashes a powerful Dark Blast! (Used 10 MP)");
        }
        target.HP -= damage;
        Console.WriteLine($"{target.Name} took {damage} skill damage!");
      }
      else if (choice == 3)
      {
        isEnemyBlocking = true;
        MP += 10;
        Console.WriteLine($"\n[Enemy Turn] {Name} takes a defensive stance and channels energy! (+10 Enemy MP)");
      }
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      TestHero player = new TestHero("Player_Alpha", 120, 15);
      TestEnemy enemy = new TestEnemy("Dungeon Guardian", 100, 20);

      string combatLog = $"Battle Started! You encounter a {enemy.Name}.";

      while (player.IsAlive && enemy.IsAlive)
      {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("         TERMINAL DUNGEON TEST          ");
        Console.WriteLine("========================================");
        Console.WriteLine($"[PLAYER] {player.Name} - HP: {player.HP}/{player.MaxHP} | MP: {player.MP}/{player.MaxMP}");
        Console.WriteLine($"[ENEMY]  {enemy.Name} - HP: {enemy.HP}/{enemy.MaxHP} | MP: {enemy.MP}/{enemy.MaxMP}");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine($"[LAST TURN]: {combatLog}");
        Console.WriteLine("----------------------------------------");

        bool validAction = false;
        bool isPlayerBlocking = false;

        while (!validAction)
        {
          Console.WriteLine("\nChoose your action:");
          Console.WriteLine("1. Attack");
          Console.WriteLine("2. Use Skill (Costs 15 MP)");
          Console.WriteLine("3. Block (Reduce damage & Regen 5 MP)");
          Console.Write("> ");

          string input = Console.ReadLine();

          if (input == "1")
          {
            Console.Clear();
            player.Attack(enemy);
            combatLog = $"{player.Name} dealt {player.ATK} damage to {enemy.Name}.";
            validAction = true;
          }
          else if (input == "2")
          {
            if (player.MP >= 15)
            {
              Console.Clear();
              player.UseSkill(enemy);
              combatLog = $"{player.Name} used Heavy Strike on {enemy.Name}.";
              validAction = true;
            }
            else
            {
              Console.WriteLine("Not enough MP! Choose another action.");
            }
          }
          else if (input == "3")
          {
            Console.Clear();
            isPlayerBlocking = true;
            player.MP += 5;
            Console.WriteLine($"\n[Action] {player.Name} raises their guard! (+5 MP)");
            combatLog = $"{player.Name} chose to Block.";
            validAction = true;
          }
          else
          {
            Console.WriteLine("Invalid choice. Please type 1, 2, or 3.");
          }
        }

        if (!enemy.IsAlive)
        {
          Console.WriteLine($"\nVictory! {enemy.Name} has been defeated!");
          break;
        }

        System.Threading.Thread.Sleep(1500);

        bool isEnemyBlocking;
        enemy.TakeTurn(player, isPlayerBlocking, out isEnemyBlocking);

        if (isEnemyBlocking)
          combatLog += $" Enemy shielded up and recharged.";
        else
          combatLog += $" Enemy counter-attacked!";

        if (!player.IsAlive)
        {
          Console.WriteLine($"\nGame Over... {player.Name} was defeated.");
          break;
        }

        System.Threading.Thread.Sleep(2500);
      }

      Console.WriteLine("\n========================================");
      Console.WriteLine("             SESSION ENDED              ");
      Console.WriteLine("========================================");
      Console.ReadLine();
    }
  }
}
