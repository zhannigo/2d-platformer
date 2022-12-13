using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Character.Scripts
{
  [RequireComponent(typeof(AnimatorController))]
  public class HeroAttack : MonoBehaviour
  {
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float _attackRange; 
    public int _attackDamage;
    
    private AnimatorController _animator;

    public void Awake() => 
      _animator = GetComponent<AnimatorController>();

    public string PlayAttack()
    {
      if (!_animator.IsAttacking)
      {
        _animator.PlayAttack();
      }

      string id = null;
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayer);
      foreach (Collider2D enemy in hitEnemies)
      {
        Debug.Log("We hit" + enemy.name);
        Enemy component;
        id = enemy.TryGetComponent<Enemy>(out component) ? component.Id : null;
      }
      return id;
    }
  }
  
}
  