using System;
using Common.Character.Scripts;
using Common.Enemy.Scripts;
using UnityEngine;

namespace Common.Infrastructure
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
      if (HeroController == null || EnemyController == null)
      {
        Debug.Log("not all controllers loaded");
      }
      else
      {
        Subscribe();
      }
    }

    private void Subscribe()
    {
      EnemyController.IsDead += Notify;
      HeroController.IsDead += Notify;
    }
    
    private void Notify() => 
      GameIsOver?.Invoke();

  }
}