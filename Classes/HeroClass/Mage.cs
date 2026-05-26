using System;

namespace TerminalDungeon
{
  public class Mage : Hero
  {
    public Mage(string name) : base(name, 85, 8, 90)
    {
    }

    public override string UseSkill(Character target)
    {
      if (MP >= 20)
      {
        MP -= 20;
        int damage = ATK + 35;
        target.HP -= damage;
        return $"{Name} casts Fireball! A blazing explosion deals {damage} magical damage.";
      }
      return $"{Name} tried to cast Fireball, but didn't have enough MP!";
    }
  }
}
