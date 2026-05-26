using System;
using System.Collections.Generic;

namespace TerminalDungeon
{
  public class Hero : Character
  {
    public int Level { get; private set; }
    public int EXP { get; private set; }
    //public List<Item> Inventory { get; private set; }

    public Hero(string name) : base(name, 100, 15)
    {
      Level = 1;
      EXP = 0;
      //Inventory = new List<Item>();
    }

    public Hero(string name, int maxHp, int atk) : base(name, maxHp, atk)
    {
      Level = 1;
      EXP = 0;
      //Inventory = new List<Item>();
    }

    public override void Attack(Character target)
    {
      Console.WriteLine($"[Attack] {Name} strikes {target.Name}!");
      target.HP -= this.ATK;
      Console.WriteLine($"{target.Name} took {this.ATK} damage (HP remaining: {target.HP}/{target.MaxHP})");
    }

    public virtual void UseSkill(Character target)
    {
      Console.WriteLine($"{Name} tries to use a basic skill, but nothing happens...");
    }

    public void GainEXP(int amount)
    {
      EXP += amount;
      Console.WriteLine($"\n[EXP] {Name} gained {amount} EXP (Total: {EXP}/100)");
      if (EXP >= 100)
      {
        LevelUp();
      }
    }

    private void LevelUp()
    {
      Level++;
      EXP -= 100;
      MaxHP += 20;
      HP = MaxHP;
      ATK += 5;
      Console.WriteLine($"Level Up! {Name} reached Level {Level}! (MaxHP +20, ATK +5, HP restored!)");
    }
  }
}
