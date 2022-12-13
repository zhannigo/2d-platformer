using System;
using Zenject;

namespace Common.Enemies.Scripts
{
  public class EnemyController 
  {
    public EnemiesData _enemies;
    
    public void Construct(EnemiesData enemies) => 
      _enemies = enemies;

    public void TakeDamage(string id, int damage)
    {
      if (_enemies.CheckEnemy(id))
      {
        _enemies.GetEnemy(id).TakeDamage(damage);
      }
    }

    public class Factory : PlaceholderFactory<EnemyController>
    {
    }
  }
}