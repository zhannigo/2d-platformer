using System;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Infrastructure.Services
{
  public class UnitService
  {
    public event  Action isWin;
    public event Action isLose;
    private HeroController _heroController;
    private EnemyController _enemyController;

    public HeroController HeroController
    {
      get => _heroController;
      set
      {
        _heroController = value;
        CheckAnother();
      }
    }

    public EnemyController EnemyController
    {
      get => _enemyController;
      set
      {
        _enemyController = value;
        CheckAnother();
      }
    }

    private void CheckAnother()
    {
      if (HeroController != null && EnemyController != null)
      {
        Subscribe();
      }
    }

    private void Subscribe()
    {
      EnemyController._enemies.IsAllEnemyDead += NotifyEnemiesDeath;
      HeroController.IsDead += NotifyHeroDeath;
    }

    private void NotifyEnemiesDeath() => 
      isWin?.Invoke();

    private void NotifyHeroDeath()
    {
      //GameIsOver?.Invoke();
      isLose?.Invoke();
    }
  }
}