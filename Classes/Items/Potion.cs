using System;

namespace TerminalDungeon
{
  public class Potion : Item
  {
    private int _healAmount;
    private int _manaAmount;

    public Potion(string name, string description, int healAmount, int manaAmount = 0) : base(name, description)
    {
      _healAmount = healAmount;
      _manaAmount = manaAmount;
    }

    public override string Use(Character user)
    {
      string msg = $"{user.Name} drank {Name}.";

      if (_healAmount > 0)
      {
        user.HP += _healAmount;
        msg += $" Restored {_healAmount} HP!";
      }

      if (_manaAmount > 0)
      {
        user.MP += _manaAmount;
        msg += $" Restored {_manaAmount} MP!";
      }

      return msg;
    }
  }
}
