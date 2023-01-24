namespace Common.Enemies.Scripts
{
  public class EnemyController 
  {
    public EnemiesData _enemies;

    public void Construct(EnemiesData enemies) => 
      _enemies = enemies;

    public void Initialize()
    {
      EnemyEventManager.OnChangedState += UpdateState;
      EnemyEventManager.OnHeroLost += ReturnToWay;
      EnemyEventManager.OnHeroFounded += MoveToHero;
      foreach (Enemy enemy in _enemies.AllEnemy())
      {
        UpdateState(enemy);
      }
    }

    private void ReturnToWay(Enemy enemy)
    {
      enemy.MoveController.ReturnToTheWay();
    }

    private void UpdateState(Enemy enemy)
    {
      switch (enemy.State)
        {
          case EnemyState.Idle:
            ChangeStateToMove(enemy);
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
      enemy.MoveController.MoveOff();
      enemy.EnemyAnimator.SetTrigger("Attack");
      enemy.AttackSound.Play();
    }

    private void Move(Enemy enemy)
    {
      enemy.MoveController.MoveOn(enemy.EnemyAnimator);
    }

    private void MoveToHero(Enemy enemy)
    {
      enemy.MoveController.MoveOff();
      var hero = enemy.HeroTrasform;
      enemy.MoveController.MoveTo(hero);
    }

    private void ChangeStateToMove(Enemy enemy) => 
      enemy.State = EnemyState.Move;

    public void TakeDamage(string id, int damage)
    {
      if (_enemies.CheckEnemy(id))
      {
        var enemy = _enemies.GetEnemy(id);
        enemy.TakeDamage(damage);
        enemy.MoveController.MoveOff();
        enemy.IsEnemyDead += Dead;
      }
    }

    private void Dead(Enemy enemy, string id)
    {
      enemy.IsEnemyDead -= Dead;
    }
  }
}