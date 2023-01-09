using System;

namespace Common.Enemies.Scripts
{
  public class EventManager
  {
    public static Action <Enemy> OnChangedState;
    
    public static void StateIsChanged(Enemy enemy)
    {
      if (OnChangedState != null) OnChangedState.Invoke(enemy);
    }
  }
}