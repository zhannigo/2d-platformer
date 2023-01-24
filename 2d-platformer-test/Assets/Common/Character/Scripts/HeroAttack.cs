using Common.Enemies.Scripts;
using Common.Infrastructure.Services;
using UnityEngine;

namespace Common.Character.Scripts
{
  public class HeroAttack : MonoBehaviour
  {
    public void Attack(Collider2D[] enemies, EnemyController enemyController, int damage)
    {
      foreach (Collider2D enemy in enemies)
      {
        var id = enemy.TryGetComponent(out Enemy component) ? component.Id : null;
        enemyController.TakeDamage(id, damage);
      }
    }
  }
}
  