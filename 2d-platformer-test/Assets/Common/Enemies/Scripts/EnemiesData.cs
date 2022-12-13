using System;
using System.Collections.Generic;
using Zenject;

namespace Common.Enemies.Scripts
{
  public class EnemiesData
  {
    private readonly Dictionary<string, Enemy> _enemies  = new Dictionary<string, Enemy>();
    public event Action IsDead;
    public void AddEnemy(string id, Enemy enemy)
    {
      _enemies.Add(id, enemy);
      enemy.isEnemyDead += delegate { RemoveEnemy(id); };
    }

    public bool CheckEnemy(string id) => 
      _enemies.ContainsKey(id);

    public Enemy GetEnemy(string id) => 
      _enemies.TryGetValue(id, out Enemy enemy) ? enemy : null;

    private void RemoveEnemy(string id)
    {
      _enemies.Remove(id);
      CheckEnemiesCount();
    }

    private void CheckEnemiesCount()
    {
      if (_enemies.Count == 0)
      {
        IsDead?.Invoke();
      }
    }
  }
}