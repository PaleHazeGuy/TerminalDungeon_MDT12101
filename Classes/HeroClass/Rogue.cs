using System;

namespace TerminalDungeon
{
  public class Rogue : Hero
  {
    private readonly Random _rng = new Random();

    public Rogue(string name) : base(name, 105, 22, 45)
    {
    }

    public override string UseSkill(Character target)
    {
      if (MP >= 15)
      {
        MP -= 15;
        bool isCrit = _rng.Next(0, 2) == 0;
        int damage = isCrit ? ATK * 3 : (int)(ATK * 1.5);

        target.HP -= damage;
        return isCrit
            ? $"{Name} lands a CRITICAL Assassinate! Pierced {target.Name} for {damage} damage!"
            : $"{Name} uses Assassinate dealing {damage} precise damage.";
      }
      return $"{Name} tried to use Assassinate, but didn't have enough MP!";
    }
  }
}
