using System;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Infrastructure.Services
{
  public class UnitService
  {
    private HeroController _heroController;
    private EnemyController _enemyController;
    public event Action GameIsOver;

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
      EnemyController._enemies.IsDead += Notify;
      HeroController.IsDead += Notify;
    }
    
    private void Notify() => 
      GameIsOver?.Invoke();

  }
}