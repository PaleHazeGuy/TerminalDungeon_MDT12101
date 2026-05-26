using System;

namespace TerminalDungeon
{
  public abstract class Enemy : Character
  {
    protected readonly Random Rng = new Random();
    public int RewardEXP { get; private set; }

    public Enemy(string name, int hp, int atk, int maxMp, int rewardExp) : base(name, hp, atk, maxMp)
    {
      RewardEXP = rewardExp;
    }

    public override string Attack(Character target)
    {
      target.HP -= ATK;
      return $"{Name} strikes {target.Name} for {ATK} damage.";
    }

    public abstract string TakeTurn(Character target, bool isTargetBlocking, out bool isEnemyBlocking);
  }
}
