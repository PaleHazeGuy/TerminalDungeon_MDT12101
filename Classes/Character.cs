using System;

namespace TerminalDungeon
{
  public abstract class Character
  {
    private string _name;
    private int _hp;
    private int _maxHp;
    private int _atk;

    public string Name
    {
      get => _name;
      protected set => _name = value;
    }

    public int MaxHP
    {
      get => _maxHp;
      protected set => _maxHp = value;
    }

    public int HP
    {
      get => _hp;
      set => _hp = Math.Clamp(value, 0, MaxHP);
    }

    public int ATK
    {
      get => _atk;
      protected set => _atk = value;
    }

    public bool IsAlive => HP > 0;
    public Character(string name, int maxHp, int atk)
    {
      Name = name;
      MaxHP = maxHp;
      HP = maxHp; ATK = atk;
    }

    public abstract void Attack(Character target);
  }
}
