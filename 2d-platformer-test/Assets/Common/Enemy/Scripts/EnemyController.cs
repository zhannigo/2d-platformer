using System;
using System.Collections.Generic;
using Common.Character.Scripts;
using Zenject;

namespace Common.Enemy.Scripts
{
  public class EnemyController 
  {
    public Dictionary<string, Enemy> _enemies  = new Dictionary<string, Enemy>();
    public event Action IsDead;

    public void AddEnemy(string id, Enemy enemy) => 
      _enemies.Add(id, enemy);

    public void TakeDamage(string id, int damage)
    {
      Enemy enemy = GetEnemy(id);
      if (enemy)
      {
        enemy.TakeDamage(damage);
        _enemies.Remove(id);
        CheckEnemy();
      }
    }

    private void CheckEnemy()
    {
      if (_enemies.Count == 0)
      {
        IsDead?.Invoke();
      }
    }

    private Enemy GetEnemy(string id) => 
      _enemies.TryGetValue(id, out Enemy enemy) ? enemy : null;

    public class Factory : PlaceholderFactory<EnemyController>
    {
    }
  }
}