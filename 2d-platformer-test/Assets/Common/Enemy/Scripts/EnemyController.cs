using System;
using System.Collections.Generic;
using Common.Enemy.Scripts;

namespace Common.Infrastructure
{
  public class EnemyController
  {
    private Dictionary<string, EnemyModel> _enemies  = new Dictionary<string, EnemyModel>();
    private EnemyModel _enemy;
    public event Action IsGameOver;

    public void AddEnemy(string id, EnemyModel enemy)
    {
      _enemies.Add(id, enemy);
    }

    public void TakeDamage(string id, int damage)
    {
      _enemy = _enemies.TryGetValue(id, out EnemyModel enemyModel) ? enemyModel : null;
      if (_enemy)
      {
        _enemy.TakeDamage(damage);
        _enemy.isEnemyDead += CheckEnemy;
      }
    }

    private void CheckEnemy()
    {
      if (_enemies.Count == 0)
      {
        IsGameOver?.Invoke();
      }
      _enemy.isEnemyDead -= CheckEnemy;
    }
    
  }
}