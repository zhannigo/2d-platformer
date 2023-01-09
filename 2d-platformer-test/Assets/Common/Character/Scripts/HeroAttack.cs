using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Character.Scripts
{
  [RequireComponent(typeof(AnimatorController))]
  public class HeroAttack : MonoBehaviour
  {
    private AnimatorController _animator;

    public void Awake() => 
      _animator = GetComponent<AnimatorController>();

    public string GetHitableEnemyIds(Collider2D[] hitEnemies)
    {
      if (!_animator.IsAttacking)
      {
        _animator.PlayAttack();
      }
      
      string id = null;
      foreach (Collider2D enemy in hitEnemies)
      {
        Debug.Log("We hit" + enemy.name);
        Enemy component;
        id = enemy.TryGetComponent(out component) ? component.Id : null;
      }
      return id;
    }
  }
  
}
  