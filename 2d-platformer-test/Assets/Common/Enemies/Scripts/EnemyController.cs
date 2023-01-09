using UnityEngine;

namespace Common.Enemies.Scripts
{
  public class EnemyController 
  {
    public EnemiesData _enemies;

    public void Construct(EnemiesData enemies) => 
      _enemies = enemies;

    public void Initialize()
    {
      EventManager.OnChangedState += UpdateState;
      foreach (Enemy enemy in _enemies.AllEnemy())
      {
        UpdateState(enemy);
      }
    }

    private void UpdateState(Enemy enemy)
    {
      switch (enemy.State)
        {
          case EnemyState.Idle:
            CheckDirection(enemy);
            break;
          case EnemyState.Move:
            Move(enemy);
            break;
          case EnemyState.Attack:
            Attack(enemy);
            break;
          case EnemyState.Hit:
            break;
        }
    }

    private void Attack(Enemy enemy)
    {
      throw new System.NotImplementedException();
    }

    private void Move(Enemy enemy)
    {
      enemy.MoveController.MoveOn();
    }

    private void CheckDirection(Enemy enemy)
    {
      enemy.State = EnemyState.Move;
    }

    public void TakeDamage(string id, int damage)
    {
      if (_enemies.CheckEnemy(id))
      {
        _enemies.GetEnemy(id).TakeDamage(damage);
      }
    }
  }
}