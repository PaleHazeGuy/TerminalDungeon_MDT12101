using System;
using System.Collections.Generic;

namespace TerminalDungeon
{
  public abstract class Hero : Character
  {
    public int Level { get; private set; }
    public int EXP { get; private set; }
    public List<Item> Inventory { get; private set; }

    public Hero(string name) : base(name, 100, 15, 40)
    {
      Level = 1;
      EXP = 0;
      Inventory = new List<Item>();
    }

    public Hero(string name, int maxHp, int atk, int maxMp) : base(name, maxHp, atk, maxMp)
    {
      Level = 1;
      EXP = 0;
      Inventory = new List<Item>();
    }

    public override string Attack(Character target)
    {
      target.HP -= this.ATK;
      return $"{Name} attacks {target.Name} for {this.ATK} damage.";
    }

    public abstract string UseSkill(Character target);

    public string UseItem(int index)
    {
      if (index < 0 || index >= Inventory.Count)
      {
        return "Invalid item choice.";
      }

      Item item = Inventory[index];
      string resultMessage = item.Use(this);
      Inventory.RemoveAt(index);
      return resultMessage;
    }

    public string GainEXP(int amount)
    {
      EXP += amount;
      string msg = $"{Name} gained {amount} EXP.";
      if (EXP >= 100)
      {
        msg += " " + LevelUp();
      }
      return msg;
    }

    private string LevelUp()
    {
      Level++;
      EXP -= 100;
      MaxHP += 20;
      HP = MaxHP;
      ATK += 5;
      MaxMP += 10;
      MP = MaxMP;
      return $"{Name} leveled up to Level {Level}! Stats fully restored.";
    }
  }
}
