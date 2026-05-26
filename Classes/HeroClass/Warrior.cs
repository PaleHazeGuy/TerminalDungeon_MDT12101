using System;

namespace TerminalDungeon
{
  public class Warrior : Hero
  {
    public Warrior(string name) : base(name, 140, 18, 30)
    {
    }

    public override string UseSkill(Character target)
    {
      if (MP >= 15)
      {
        MP -= 15;
        int damage = ATK * 2;
        target.HP -= damage;
        return $"{Name} unleashes a Shield Slam! Dealt {damage} skill damage.";
      }
      return $"{Name} tried to use Shield Slam, but didn't have enough MP!";
    }
  }
}
