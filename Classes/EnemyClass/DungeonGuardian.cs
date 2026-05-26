namespace TerminalDungeon
{
  public class DungeonGuardian : Enemy
  {
    public DungeonGuardian() : base("Dungeon Guardian", 110, 18, 30, 120)
    {
    }

    public override string TakeTurn(Character target, bool isTargetBlocking, out bool isEnemyBlocking)
    {
      isEnemyBlocking = false;
      int choice = Rng.Next(1, 4);

      if (choice == 2 && MP < 10)
      {
        choice = 1;
      }

      if (choice == 1)
      {
        int damage = ATK;
        if (isTargetBlocking)
        {
          damage = (int)(ATK * 0.3);
          target.HP -= damage;
          return $"{Name} lunges forward, but you blocked the hit, taking only {damage} damage.";
        }
        target.HP -= damage;
        return $"{Name} lunges forward with a basic attack dealing {damage} damage.";
      }
      else if (choice == 2)
      {
        MP -= 10;
        int damage = ATK + 12;
        if (isTargetBlocking)
        {
          damage = (int)(damage * 0.3);
          target.HP -= damage;
          return $"{Name} unleashes a Dark Blast! You mitigated the impact, taking {damage} damage.";
        }
        target.HP -= damage;
        return $"{Name} unleashes a powerful Dark Blast for {damage} damage!";
      }
      else
      {
        isEnemyBlocking = true;
        MP += 10;
        return $"{Name} takes a defensive stance and channels energy (+10 Enemy MP).";
      }
    }
  }
}
