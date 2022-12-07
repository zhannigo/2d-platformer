using System;
using System.Collections;
using Common.Enemy.Scripts;
using UnityEngine;

namespace Common.Character.Scripts
{
  public class HeroAttack : MonoBehaviour
  {
    public Transform attackPoint;
    public LayerMask enemyLayer; //static data
    public float _attackRange; //static data
    public int _attackDamage; //static data

    private AnimatorController _animator;

    public void PlayAttack()
    {
      if (!_animator.IsAttacking)
      {
        _animator.PlayAttack();
      }

      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayer);
      foreach (Collider2D enemy in hitEnemies)
      {
        Debug.Log("We hit"+ enemy.name);
        enemy.GetComponent<IHealth>().TakeDamage(_attackDamage);
      }
    }
  }
}