using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Common.Enemies.Scripts
{
  public class EnemiesData
  {
    private readonly Dictionary<string, Enemy> _enemies  = new Dictionary<string, Enemy>();
    public event Action IsDead;
    public void AddEnemy(string id, Enemy enemy)
    {
      _enemies.Add(id, enemy);
      enemy.isEnemyDead += RemoveEnemy;
    }

    public bool CheckEnemy(string id) => 
      _enemies.ContainsKey(id);

    public Enemy GetEnemy(string id) => 
      _enemies.TryGetValue(id, out Enemy enemy) ? enemy : null;

    public List<Enemy> GetAllEnemies => _enemies.Values.ToList();

    public Dictionary<string, Enemy>.ValueCollection AllEnemy() => 
      _enemies.Values;

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