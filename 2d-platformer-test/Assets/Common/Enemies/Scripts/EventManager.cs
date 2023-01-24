using System;

namespace Common.Enemies.Scripts
{
  public class EnemyEventManager
  {
    public static Action <Enemy> OnChangedState;
    public static Action<Enemy> OnHeroFounded;
    public static Action<Enemy> OnHeroLost;

    public static void StateIsChanged(Enemy enemy)
    {
      if (OnChangedState != null) OnChangedState.Invoke(enemy);
    }

    public static void HeroIsFounded(Enemy enemy)
    {
      if (OnHeroFounded != null) OnHeroFounded.Invoke(enemy);
    }

    public static void HeroisLost(Enemy enemy)
    {
      if (OnHeroLost != null) OnHeroLost.Invoke(enemy);
    }
  }
}